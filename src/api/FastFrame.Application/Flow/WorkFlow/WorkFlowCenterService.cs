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
using System.Linq.Dynamic.Core;

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
                flowInstance = await flowInstances.AddAsync(new FlowInstance
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
                });

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

            /*取出流程配置*/
            IEnumerable<FlowNodeModel> flowNodeModels = Array.Empty<FlowNodeModel>();
            if (work_flow_id != null)
                flowNodeModels = await loader.GetService<IEventBus>().RequestAsync<FlowNodeModel[], string>(work_flow_id);

            /*匹配到的流程*/
            var match_nodes = MatchNodes(flowNodeModels, bill_Entity).ToArray();

            /*下一个节点*/
            var curr_node = MatchNextNode(match_nodes, bill_Entity, null);

            /*生成流程快照*/
            await loader
                .GetService<HandleOne2ManyService<FlowSnapshot, FlowSnapshot>>()
                .UpdateManyAsync(
                    v => v.FlowInstance_Id == flowInstance.Id,
                     match_nodes.Select((v, i) => new FlowSnapshot
                     {
                         FlowInstance_Id = flowInstance.Id,
                         OrderVal = i,
                         FlowNode_Id = v.Id,
                         Id = null
                     }),
                    (a, b) => a.FlowNode_Id == b.FlowNode_Id,
                    v => v,
                    (before, after) =>
                    {
                        before.OrderVal = after.OrderVal;
                    }
                );

            /*计算审核人*/


            /*流程走完了*/
            if (curr_node == null)
            {
                flowInstance.IsComlete = true;
                flowInstance.CompleteTime = DateTime.Now;
                flowInstance.Status = FlowStatusEnum.finished;
                flowInstance.CurrNode_Id = null;
                bill_Entity.FlowStatus = FlowStatusEnum.finished;
            }
            else
            {
                flowInstance.CurrNode_Id = curr_node.Id;
            }

            await flowInstances.UpdateAsync(flowInstance);
            await loader.GetService<IRepository<TBillEntity>>().UpdateAsync(bill_Entity);

            /*生成审批事件[审批中]*/ 

            if (auto_tran)
            {
                await flowInstances.CommmitAsync();

                /*生成审核事件[审批后]*/

            }
        }

        /// <summary>
        /// 计算下一个流程节点
        /// </summary> 
        private static FlowNodeModel MatchNextNode<TBillEntity>(IEnumerable<FlowNodeModel> match_nodes, TBillEntity bill_Entity, string curr_node_id)
            where TBillEntity : class, IHaveCheck, new()
        {
            return match_nodes.SkipWhile(v => v.Id == curr_node_id || curr_node_id.IsNullOrWhiteSpace()).FirstOrDefault();
        }

        /// <summary>
        /// 计算流程节点
        /// </summary>  
        private static IEnumerable<FlowNodeModel> MatchNodes<TBillEntity>(IEnumerable<FlowNodeModel> nodes, TBillEntity bill_Entity)
            where TBillEntity : class, IHaveCheck, new()
        {
            if (nodes == null || !nodes.Any())
                yield break;

            foreach (var item in nodes)
            {
                switch (item.NodeEnum)
                {
                    case FlowNodeEnum.check:
                    case FlowNodeEnum.cc:
                        yield return item;
                        break;
                    case FlowNodeEnum.branch:
                        /*确定走哪个分支*/
                        var child_brahchs = item
                            .Nodes
                            .OrderByDescending(v => v.IsDefault == true ? -1 : Math.Max(v.Weight ?? 0, 0))
                            .ThenBy(v => v.OrderVal);
                        var branch = child_brahchs.FirstOrDefault(v => MatchBrahsh(v, bill_Entity));
                        var children = MatchNodes(branch.Nodes, bill_Entity);
                        foreach (var child in children)
                            yield return child;
                        break;
                    case FlowNodeEnum.branch_child:
                    case FlowNodeEnum.cond:
                    case FlowNodeEnum.start:
                    case FlowNodeEnum.end:
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 判断子分支是否满足
        /// </summary>
        /// <typeparam name="TBillEntity"></typeparam>
        /// <param name="flowNode"></param>
        /// <param name="bill_Entity"></param>
        /// <returns></returns>
        private static bool MatchBrahsh<TBillEntity>(FlowNodeModel flowNode, TBillEntity bill_Entity)
             where TBillEntity : class, IHaveCheck, new()
        {
            if (flowNode.NodeEnum != FlowNodeEnum.branch_child)
                throw new MsgException("分支的下级类型不正确");

            if (flowNode.IsDefault == true)
                return true;

            var cond_node = flowNode.Nodes.FirstOrDefault(v => v.NodeEnum == FlowNodeEnum.cond);
            if (cond_node == null)
                throw new MsgException("未定义条件分支");

            if (cond_node.Conds == null || !cond_node.Conds.Any())
                return true;


            foreach (var g in cond_node.Conds)
            {
                if (g == null || !g.Any())
                    continue;

                var query = new[] { bill_Entity }.AsQueryable();

                foreach (var cond in g)
                {
                    if (cond.FieldName.IsNullOrWhiteSpace())
                        continue;

                    if (cond.Value_Id.IsNullOrWhiteSpace() && cond.ValueText.IsNullOrWhiteSpace())
                        continue;

                    if (typeof(TBillEntity).GetProperty(cond.FieldName) == null)
                        continue;

                    /*要比较的值*/
                    object value = null;
                    switch (cond.ValueEnum)
                    {
                        case FlowNodeCondValueEnum.input_value:
                            value = cond.Value_Id ?? cond.ValueText;
                            break;
                        case FlowNodeCondValueEnum.form_field:
                            value = bill_Entity.GetValue(cond.Value_Id);
                            break;
                        default:
                            break;
                    }

                    switch (cond.CompareEnum)
                    {
                        case FlowNodeCondCompareEnum.gte:
                            query = query.Where($"{cond.FieldName}>=@0", value);
                            break;
                        case FlowNodeCondCompareEnum.lte:
                            query = query.Where($"{cond.FieldName}<=@0", value);
                            break;
                        case FlowNodeCondCompareEnum.eq:
                            query = query.Where($"{cond.FieldName}==@0", value);
                            break;
                        case FlowNodeCondCompareEnum.not_eq:
                            query = query.Where($"{cond.FieldName}!=@0", value);
                            break;
                        case FlowNodeCondCompareEnum.like:
                            query = query.Where($"{cond.FieldName}.Contains(@0)", value);
                            break;
                        case FlowNodeCondCompareEnum.not_like:
                            query = query.Where($"!{cond.FieldName}.Contains(@0)", value);
                            break;
                        default:
                            break;
                    }
                }

                if (query.Any())
                    return true;
            }

            return false;
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
