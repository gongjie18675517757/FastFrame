using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using FastFrame.Entity;
using FastFrame.Repository;
using FastFrame.Entity.Flow;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure;
using System.Collections.Generic;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 流程中心
    /// </summary>
    public partial class WorkFlowCenterService : IService
    {
        private readonly IServiceProvider loader;

        public WorkFlowCenterService(IServiceProvider loader)
        {
            this.loader = loader;
        }

        /// <summary>
        /// 提交流程
        /// </summary> 
        public async Task SubmitFlow(string moduleName, string[] bill_ids, SubmitFlowInput input)
        {
            if (!Infrastructure.Module.TypeManger.TryGetType(moduleName, out var type))
                throw new Infrastructure.NotFoundException();

            if (!typeof(IHaveCheck).IsAssignableFrom(type))
                throw new Infrastructure.NotFoundException();

            if (!type.IsClass)
                throw new Infrastructure.NotFoundException();

            if (type.IsAbstract)
                throw new Infrastructure.NotFoundException();

            var method = GetType()
                .GetMethods()
                .Where(v => v.Name == nameof(SubmitFlow))
                .FirstOrDefault(v => v.IsPublic && v.IsGenericMethod && v.GetParameters()[0].ParameterType == typeof(string[]));

            method = method.MakeGenericMethod(type);
            await (Task)method.Invoke(this, new object[] { bill_ids, input });
        }

        /// <summary>
        /// 提交流程
        /// </summary> 
        public async Task SubmitFlow<TBillEntity>(string[] bill_ids, SubmitFlowInput input) where TBillEntity : class, IHaveCheck, new()
        {
            foreach (var bill_id in bill_ids)
                await SubmitFlow<TBillEntity>(bill_id, input, false);


        }

        /// <summary>
        /// 提交流程
        /// </summary> 
        public async Task SubmitFlow<TBillEntity>(string bill_id, SubmitFlowInput input, bool auto_tran = true) where TBillEntity : class, IHaveCheck, new()
        {
            var bill_Entity = await loader.GetService<IRepository<TBillEntity>>().GetAsync(bill_id);
            if (bill_Entity == null)
                throw new Infrastructure.NotFoundException();

            await SubmitFlow(bill_Entity, input, auto_tran);
        }

        /// <summary>
        /// 提交流程
        /// </summary> 
        public async Task SubmitFlow<TBillEntity>(TBillEntity bill_Entity, SubmitFlowInput input, bool auto_tran)
            where TBillEntity : class, IHaveCheck, new()
        {
            if (bill_Entity == null)
                throw new Infrastructure.NotFoundException();

            var applicationSession = loader.GetService<Infrastructure.Interface.IApplicationSession>();
            var curr = applicationSession?.CurrUser;

            var moduleDesProvider = loader.GetService<Infrastructure.Module.IModuleDesProvider>();
            var flowInstances = loader.GetService<IRepository<FlowInstance>>();
            var flowSteps = loader.GetService<IRepository<FlowStep>>();
            var flowInstanceUsers = loader.GetService<IRepository<FlowInstanceUser>>();

            /*判断单据是否提交过了*/
            var flowInstance = await flowInstances.FirstOrDefaultAsync(v => v.Bill_Id == bill_Entity.Id);
            if (flowInstance != null && !new FlowStatusEnum[] { }.Contains(flowInstance.Status))
                throw new Infrastructure.MsgException("此单据已提交过了，不可重复提交流程!");

            /*当前模块名称*/
            var beModuleName = typeof(TBillEntity).Name;
            /*最后的审核步骤*/
            FlowStep flowStep = null;

            /*计算匹配流程*/
            var work_flow_id = await loader
                .GetService<IRepository<WorkFlow>>()
                .Where(v => v.BeModule == beModuleName)
                .Where(v => v.Enabled == EnabledMark.enabled)
                .OrderByDescending(v => v.Version)
                .Select(v => v.Id)
                .FirstOrDefaultAsync();

            /*创建流程实例*/
            if (flowInstance == null)
                flowInstance = new FlowInstance
                {
                    BeModuleName = beModuleName,
                    BeModuleText = moduleDesProvider.GetClassDescription(typeof(TBillEntity)),
                    BillNumber = null,// (bill_Entity is IHaveNumber haveNumber)
                    Bill_Id = bill_Entity.Id,
                    Id = null,
                    CompleteTime = null,
                    CurrNode_Id = null,
                    IsComlete = false,
                    LastChecker_Id = null,
                    LastCheckTime = null,
                    Sponsor_Id = curr?.Id,
                    StartTime = DateTime.Now,
                    Status = FlowStatusEnum.processing,
                    WorkFlow_Id = work_flow_id
                };

            /*写入流程实际状态*/
            flowInstance.WorkFlow_Id = work_flow_id;
            flowInstance.Sponsor_Id = curr?.Id;
            flowInstance.StartTime = DateTime.Now;
            flowInstance.Status = FlowStatusEnum.processing;
            flowInstance.IsComlete = false;


            /*写入单号*/
            if (bill_Entity is IHaveNumber haveNumber)
                flowInstance.BillNumber = haveNumber.Number;

            /*写入单据摘要*/
            var workFlowDescriptionProvider = loader.GetService<IWorkFlowDescriptionProvider<TBillEntity>>();
            if (workFlowDescriptionProvider != null)
                flowInstance.BillDes = await workFlowDescriptionProvider.GetDescription(bill_Entity);
            else
                flowInstance.BillDes = bill_Entity.GetDescription();

            /*写入归属部门*/
            string[] dept_ids = null;
            if (bill_Entity is IHaveDept haveDept)
                dept_ids = haveDept.GetBeDeptIds();
            else if (bill_Entity is IHasManage hasManage)
                dept_ids = await loader
                    .GetService<IRepository<Entity.Basis.DeptMember>>()
                    .Where(v => v.User_Id == hasManage.Create_User_Id)
                    .Select(v => v.Dept_Id)
                    .Distinct()
                    .ToArrayAsync();
            else if (curr != null)
                dept_ids = await loader
                    .GetService<IRepository<Entity.Basis.DeptMember>>()
                    .Where(v => v.User_Id == curr.Id)
                    .Select(v => v.Dept_Id)
                    .Distinct()
                    .ToArrayAsync();
            await loader
                .GetService<HandleOne2ManyService<string, FlowBeDept>>()
                .UpdateManyAsync(
                    v => v.FlowInstance_Id == flowInstance.Id,
                    dept_ids,
                    (a, b) => a.BeDept_Id == b,
                    v => new FlowBeDept
                    {
                        BeDept_Id = v,
                        Id = null,
                        FlowInstance_Id = flowInstance.Id
                    }
                );

            /*生成流程快照*/
            WorkFlowDto workFlowDto = null;
            if (work_flow_id != null)
                workFlowDto = await loader.GetService<IEventBus>().RequestAsync<WorkFlowDto, string>(work_flow_id);
            await loader
                .GetService<HandleOne2ManyService<string, FlowSnapshot>>()
                .UpdateManyAsync(
                    v => v.FlowInstance_Id == flowInstance.Id,
                    workFlowDto != null ? new[] { workFlowDto.Nodes?.ToJson() } : null,
                    (a, b) => true,
                    v => new FlowSnapshot
                    {
                        FlowInstance_Id = flowInstance.Id,
                        SnapshotContent = v
                    },
                    (before, after) =>
                    {
                        before.SnapshotContent = after;
                    }
                );


            /*写入流程步骤*/
            flowStep = await flowSteps.AddAsync(new FlowStep
            {
                Action = FlowActionEnum.submit,
                Desc = input?.Des,
                FlowInstance_Id = flowInstance.Id,
                FlowNodeName = "提交",
                FlowNode_Id = null,
                Id = null,
                Operater_Id = curr?.Id,
                OperateTime = DateTime.Now
            });

            /*未匹配到流程,默认完结*/
            if (work_flow_id == null)
            {
                flowStep = await flowSteps.AddAsync(new FlowStep
                {
                    Action = FlowActionEnum.pass,
                    Desc = "未匹配到流程,自动通过",
                    FlowInstance_Id = flowInstance.Id,
                    FlowNodeName = "自动通过",
                    FlowNode_Id = null,
                    Id = null,
                    Operater_Id = curr?.Id,
                    OperateTime = DateTime.Now,
                }); 
            } 



        } 

        /// <summary>
        /// 匹配下一个节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="curr_node_id"></param>
        /// <returns></returns>
        private FlowNodeModel MatchNextNode(IEnumerable<FlowNodeModel> nodes,string curr_node_id)
        {
            return null;
        }
    }

    public class SubmitFlowInput
    {
        /// <summary>
        /// 提交说明
        /// </summary>
        public string Des { get; set; }
    }
}
