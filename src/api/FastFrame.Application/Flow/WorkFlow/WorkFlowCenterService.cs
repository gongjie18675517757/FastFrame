using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Lock;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

        public Task<PageList<FlowInstance>> PageList(Pagination pagination)
        {
            var applicationSession = loader.GetService<Infrastructure.Interface.IApplicationSession>();
            var curr = applicationSession?.CurrUser;

            var flowStepCheckers = loader.GetService<IRepository<FlowStepChecker>>().Where(v => v.User_Id == curr.Id);
            var flowSteps1 = loader.GetService<IRepository<FlowStep>>().Where(v => v.Operater_Id == curr.Id);
            var flowSteps = loader.GetService<IRepository<FlowStep>>().Where(v => flowStepCheckers.Any(x => x.FlowStep_Id == v.Id));

            return loader
                 .GetService<IRepository<FlowInstance>>()
                 .Where(v => flowSteps.Any(x => !x.IsFinished && x.FlowInstance_Id == v.Id && x.FlowNode_Id == v.CurrNode_Id) ||
                             flowSteps1.Any(x => x.FlowInstance_Id == v.Id))
                 .PageListAsync(pagination);
        }

        /// <summary>
        /// 流程操作[批量]
        /// </summary> 
        public IAsyncEnumerable<FlowOperateOutput> HandleFlowOperate(string moduleName, BatchFlowOperateInput input)
        {
            if (!Infrastructure.Module.TypeManger.TryGetType(moduleName, out var type))
                throw new NotFoundException();

            if (!typeof(IHaveCheck).IsAssignableFrom(type))
                throw new NotFoundException();

            if (!type.IsClass)
                throw new NotFoundException();

            if (type.IsAbstract)
                throw new NotFoundException();

            var method = GetType()
                .GetMethods()
                .Where(v => v.Name == nameof(BatchHandleFlowOperate))
                .FirstOrDefault(v => v.IsPublic && v.IsGenericMethod);

            method = method.MakeGenericMethod(type);
            return (IAsyncEnumerable<FlowOperateOutput>)method.Invoke(this, new object[] { input });
        }

        /// <summary>
        /// 流程操作[批量]
        /// </summary> 
        public async IAsyncEnumerable<FlowOperateOutput> BatchHandleFlowOperate<TBillEntity>(BatchFlowOperateInput input)
            where TBillEntity : class, IHaveCheck, new()
        {
            var flowSteps = loader.GetService<IRepository<FlowStep>>();
            var flowInstances = loader.GetService<IRepository<FlowInstance>>();
            var logger = loader.GetService<ILogger<WorkFlowCenterService>>();
            foreach (var bill_id in input.Keys)
            {
                FlowOperateOutput output = null;
                try
                {
                    var flowInstance = await HandleFlowOperate<TBillEntity>(bill_id, input, true);
                    var stepList = await flowSteps
                        .Where(v => flowInstances.Any(x => x.Bill_Id == bill_id && x.Id == v.FlowInstance_Id))
                        .OrderBy(v => v.Id)
                        .ToListAsync();

                    output = new FlowOperateSuccessOutput(bill_id, flowInstance.Status, flowInstance.BillNumber, stepList); 
                }
                catch (NotFoundException)
                {
                    output = new FlowOperateFailOutput(bill_id, "未找到单据或者单据已过期");
                }
                catch (MsgException ex)
                {
                    output = new FlowOperateFailOutput(bill_id, ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "流程审核失败");
                    output = new FlowOperateFailOutput(bill_id, "发生未知错误!");
                }

                yield return output;
            }
        }

        /// <summary>
        /// 流程操作
        /// </summary> 
        public async Task<FlowInstance> HandleFlowOperate<TBillEntity>(string bill_id, FlowOperateInput input, bool auto_tran = true)
            where TBillEntity : class, IHaveCheck, new()
        {
            var bill_Entity = await loader.GetService<IRepository<TBillEntity>>().GetAsync(bill_id);
            if (bill_Entity == null)
                throw new Infrastructure.NotFoundException();

            return await HandleFlowOperate(bill_Entity, input, auto_tran);
        }


        /// <summary>
        /// 流程操作
        /// </summary> 
        public async Task<FlowInstance> HandleFlowOperate<TBillEntity>(TBillEntity bill_Entity, FlowOperateInput input, bool auto_tran)
            where TBillEntity : class, IHaveCheck, new()
        {
            if (bill_Entity == null)
                throw new NotFoundException();

            /*加并发锁*/
            var lockHolder = await loader.GetService<ILockFacatory>().TryCreateLockAsync(bill_Entity.Id, TimeSpan.FromSeconds(3));
            if (lockHolder == null)
                throw new MsgException("此单据正在进行流程流转,请稍后再试!");

            try
            {
                /*生成审批事件[审批前]*/
                await loader.GetService<IEventBus>().TriggerEventAsync(new FlowOperateBefore<TBillEntity>(bill_Entity, input));

                var applicationSession = loader.GetService<Infrastructure.Interface.IApplicationSession>();
                var curr = applicationSession?.CurrUser;

                /*记录流程是否完结了*/
                var flow_state = bill_Entity.FlowStatus;

                var moduleDesProvider = loader.GetService<Infrastructure.Module.IModuleDesProvider>();
                var flowInstances = loader.GetService<IRepository<FlowInstance>>();
                var flowSteps = loader.GetService<IRepository<FlowStep>>();
                var flowStepCheckers = loader.GetService<IRepository<FlowStepChecker>>();
                var flowNodes = loader.GetService<IRepository<FlowNode>>();

                /*取流程实例*/
                var flowInstance = await flowInstances.FirstOrDefaultAsync(v => v.Bill_Id == bill_Entity.Id);

                /*清除未完成的步骤(每次要重算后面的步骤)*/
                if (flowInstance != null)
                {
                    var stepFilter = flowSteps.Where(v => v.FlowInstance_Id == flowInstance.Id && !v.IsFinished);
                    var beforeStepList = await stepFilter.ToListAsync();
                    var beforeCheckerList = await flowStepCheckers.Where(v => stepFilter.Any(x => v.FlowStep_Id == v.Id)).ToListAsync();

                    foreach (var item in beforeStepList)
                        await flowSteps.DeleteAsync(item);

                    foreach (var item in beforeCheckerList)
                        await flowStepCheckers.DeleteAsync(item);
                }


                /*验证状态*/
                switch (input.ActionEnum)
                {
                    case FlowActionEnum.submit:
                        {
                            if (flowInstance != null &&
                               !new FlowStatusEnum[] { FlowStatusEnum.unsubmitted, FlowStatusEnum.ng }.Contains(flowInstance.Status))
                                throw new MsgException("此单据已提交过了，不可重复提交流程!");

                            /*当前模块名称*/
                            var beModuleName = typeof(TBillEntity).Name;

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
                            {
                                flowInstance = await flowInstances.AddAsync(new FlowInstance
                                {
                                    BeModuleName = beModuleName,
                                    BeModuleText = moduleDesProvider.GetClassDescription(typeof(TBillEntity)),
                                    BillNumber = bill_Entity.Number,
                                    Bill_Id = bill_Entity.Id,
                                    BillDes = bill_Entity.GetDescription(),
                                    Id = null,
                                    CompleteTime = null,
                                    CurrNode_Id = null,
                                    CurrNodeName = null,
                                    IsComlete = false,
                                    LastChecker_Id = null,
                                    LastCheckTime = null,
                                    LastCheckerName = null,
                                    Sponsor_Id = curr?.Id,
                                    SponsorName = curr?.Name,
                                    StartTime = DateTime.Now,
                                    Status = FlowStatusEnum.processing,
                                    WorkFlow_Id = work_flow_id,
                                });
                            }
                            /*更新流程实际状态*/
                            else
                            {
                                flowInstance.BillDes = bill_Entity.GetDescription();
                                flowInstance.BillNumber = bill_Entity.Number;
                                flowInstance.WorkFlow_Id = work_flow_id;
                                flowInstance.Sponsor_Id = curr?.Id;
                                flowInstance.SponsorName = curr?.Name;
                                flowInstance.StartTime = DateTime.Now;
                                flowInstance.Status = FlowStatusEnum.processing;
                                flowInstance.IsComlete = false;
                                SetCurrNote(flowInstance, null);
                            }

                            /*更新单据摘要*/
                            var workFlowDescriptionProvider = loader.GetService<IWorkFlowDescriptionProvider<TBillEntity>>();
                            if (workFlowDescriptionProvider != null)
                                flowInstance.BillDes = await workFlowDescriptionProvider.GetDescription(bill_Entity);

                            /*写入步骤*/
                            await flowSteps.AddAsync(new FlowStep
                            {
                                Action = FlowActionEnum.submit,
                                Desc = input.Desc,
                                FlowInstance_Id = flowInstance.Id,
                                FlowNodeName = "提交流程",
                                FlowNode_Id = null,
                                Id = null,
                                Operater_Id = curr?.Id,
                                OperaterName = curr?.Name,
                                OperateTime = DateTime.Now,
                                BeForm_Id = null,
                                IsFinished = true,
                            });

                            /*取出流程配置*/
                            FlowNodeModel[] match_nodes = await MatchNextFlowNotes(bill_Entity, work_flow_id, null);

                            /*说明没有有效节点,审核就完成了*/
                            if (match_nodes.Length == 0)
                            {
                                SetFlowCompalte(flowInstance, FlowStatusEnum.pass);

                                /*写入步骤*/
                                await flowSteps.AddAsync(new FlowStep
                                {
                                    Action = null,
                                    Desc = "未定义流程,提交即完结",
                                    FlowInstance_Id = flowInstance.Id,
                                    FlowNodeName = "缺省节点",
                                    FlowNode_Id = null,
                                    Id = null,
                                    Operater_Id = curr?.Id,
                                    OperaterName = curr?.Name,
                                    OperateTime = DateTime.Now,
                                    BeForm_Id = null,
                                    IsFinished = true,
                                });
                            }

                            /*写入新的步骤*/
                            else
                            {
                                SetCurrNote(flowInstance, match_nodes[0]);
                                await WriteNextStep(bill_Entity, flowInstance, match_nodes, null, null);
                            }
                        }

                        break;
                    case FlowActionEnum.unsubmit:
                        {
                            if (flowInstance == null)
                                throw new MsgException("流程还未提交!");

                            if (flowInstance.Sponsor_Id != curr.Id)
                                throw new MsgException("流程不是由您提交的,无法撤回!");

                            if (flowInstance.Status != FlowStatusEnum.processing)
                                throw new MsgException("审核中的流程才可以撤回!");

                            await flowSteps.AddAsync(new FlowStep
                            {
                                FlowInstance_Id = flowInstance.Id,
                                Desc = input.Desc,
                                BeForm_Id = null,
                                Action = FlowActionEnum.unsubmit,
                                FlowNodeName = await flowNodes
                                    .Where(v => v.Id == flowInstance.CurrNode_Id)
                                    .Select(v => v.Title)
                                    .FirstOrDefaultAsync(),
                                FlowNode_Id = flowInstance.CurrNode_Id,
                                IsFinished = true,
                                OperaterName = curr?.Name,
                                Operater_Id = curr?.Id,
                                OperateTime = DateTime.Now,
                            });

                            SetFlowCompalte(flowInstance, FlowStatusEnum.unsubmitted);
                        }

                        break;
                    case FlowActionEnum.pass:
                    case FlowActionEnum.ng:
                        {
                            if (flowInstance == null)
                                throw new MsgException("流程还未提交!");

                            if (flowInstance.Status != FlowStatusEnum.processing)
                                throw new MsgException("流程未在审核中!");

                            /*匹配到的流程*/
                            var match_nodes = await MatchNextFlowNotes(bill_Entity, flowInstance.WorkFlow_Id, flowInstance.CurrNode_Id);

                            if (match_nodes.Length == 0)
                                throw new MsgException("计算流程步骤失败,请联系管理员!");

                            var curr_node = match_nodes[0];

                            /*计算可审批人*/
                            var checkerIds = await MatchCheckerIds(curr_node, bill_Entity, flowInstance.Sponsor_Id).ToListAsync();
                            var ids = checkerIds.Where(v => !v.IsNullOrWhiteSpace()).Distinct().ToArray();
                            if (!ids.Contains(curr?.Id))
                            {
                                throw new MsgException("当前步骤您并没有权限审核,可能是由于单据审批期间您的部门/角色发生了变化!");
                            }

                            /*写入步骤*/
                            await flowSteps.AddAsync(new FlowStep
                            {
                                Action = input.ActionEnum,
                                Desc = input.Desc,
                                FlowInstance_Id = flowInstance.Id,
                                FlowNodeName = curr_node.Title,
                                FlowNode_Id = curr_node.Id,
                                Id = null,
                                Operater_Id = curr?.Id,
                                OperaterName = curr?.Name,
                                OperateTime = DateTime.Now,
                                BeForm_Id = null,
                                IsFinished = true,
                            });

                            /*更新最后审核人*/
                            flowInstance.LastCheckTime = DateTime.Now;
                            flowInstance.LastChecker_Id = curr?.Id;
                            flowInstance.LastCheckerName = curr?.Name;

                            /*审核模式*/
                            var check_mode = curr_node.CheckEnum ?? FlowNodeCheckEnum.or;

                            /*考虑会签/或签/投票*/
                            switch (check_mode)
                            {
                                case FlowNodeCheckEnum.or:
                                    {
                                        switch (input.ActionEnum)
                                        {
                                            case FlowActionEnum.pass:
                                                {
                                                    var next_notes = match_nodes.Skip(1).ToArray();

                                                    /*说明审核完了*/
                                                    if (next_notes.Length == 0)
                                                    {
                                                        SetFlowCompalte(flowInstance, FlowStatusEnum.pass);
                                                    }
                                                    /*说明后面还有步骤*/
                                                    else
                                                    {
                                                        SetCurrNote(flowInstance, next_notes[0]);
                                                        await WriteNextStep(bill_Entity, flowInstance, next_notes, input.NextCheckerIds, null);
                                                    }
                                                }
                                                break;
                                            case FlowActionEnum.ng:
                                                SetFlowCompalte(flowInstance, FlowStatusEnum.ng);
                                                break;
                                        }
                                    }
                                    break;
                                case FlowNodeCheckEnum.and:
                                    {
                                        switch (input.ActionEnum)
                                        {
                                            case FlowActionEnum.pass:
                                                {
                                                    var checked_steps = await GetNoteCheckeSteps(flowInstance.Id, flowInstance.CurrNode_Id, ids);


                                                    /*所有的人都审核完了,进入下一个步骤*/
                                                    if (ids
                                                        .Where(v => v != curr?.Id)
                                                        .All(v => checked_steps
                                                                    .Any(x => x.Action == FlowActionEnum.pass && x.Operater_Id == v)))
                                                    {
                                                        var next_notes = match_nodes.Skip(1).ToArray();

                                                        /*审核完了*/
                                                        if (next_notes.Length == 0)
                                                        {
                                                            SetFlowCompalte(flowInstance, FlowStatusEnum.pass);
                                                        }

                                                        /*还有下面步骤*/
                                                        else
                                                        {
                                                            await WriteNextStep(bill_Entity, flowInstance, next_notes, input.NextCheckerIds, null);
                                                            SetCurrNote(flowInstance, next_notes[0]);
                                                        }
                                                    }
                                                    /*当前节点还没审核完*/
                                                    else
                                                    {
                                                        var next_notes = match_nodes;
                                                        var checked_userids = checked_steps.Select(v => v.Operater_Id).Append(curr?.Id).ToArray();
                                                        await WriteNextStep(bill_Entity, flowInstance, next_notes, input.NextCheckerIds, checked_userids);
                                                    }

                                                }
                                                break;
                                            case FlowActionEnum.ng:
                                                SetFlowCompalte(flowInstance, FlowStatusEnum.ng);
                                                break;
                                        }
                                    }
                                    break;
                                case FlowNodeCheckEnum.vote:
                                    {
                                        var vote_scale = curr_node.VoteScale ?? 1;
                                        var checked_steps = await GetNoteCheckeSteps(flowInstance.Id, flowInstance.CurrNode_Id, ids);


                                        /*所有的人都审核完了,进入下一个步骤*/
                                        if (ids
                                            .Where(v => v != curr?.Id)
                                            .All(v => checked_steps
                                                        .Any(x => x.Action == FlowActionEnum.pass && x.Operater_Id == v)))
                                        {
                                            var pass_count = checked_steps
                                                 .Select(v => v.Action)
                                                 .Append(input.ActionEnum)
                                                 .Where(v => v != null && v.Value == FlowActionEnum.pass)
                                                 .Count();

                                            /*投票结果通过*/
                                            if (pass_count >= ids.Length * vote_scale)
                                            {
                                                var next_notes = match_nodes.Skip(1).ToArray();

                                                /*审核完了*/
                                                if (next_notes.Length == 0)
                                                {
                                                    SetFlowCompalte(flowInstance, FlowStatusEnum.pass);
                                                }

                                                /*还有下面步骤*/
                                                else
                                                {
                                                    await WriteNextStep(bill_Entity, flowInstance, next_notes, input.NextCheckerIds, null);
                                                    SetCurrNote(flowInstance, next_notes[0]);
                                                }
                                            }

                                            /*投票结果不通过*/
                                            else
                                            {
                                                SetFlowCompalte(flowInstance, FlowStatusEnum.ng);
                                            }
                                        }
                                        /*当前节点还没审核完*/
                                        else
                                        {
                                            var next_notes = match_nodes;
                                            var checked_userids = checked_steps.Select(v => v.Operater_Id).Append(curr?.Id).ToArray();
                                            await WriteNextStep(bill_Entity, flowInstance, next_notes, input.NextCheckerIds, checked_userids);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                        break;
                    case FlowActionEnum.uncheck:
                        {
                            if (flowInstance == null)
                                throw new MsgException("流程还未提交!");

                            /*上一个审核的节点*/
                            var prev_note_id = await flowSteps
                                .Where(v => v.FlowInstance_Id == flowInstance.Id)
                                .Where(v => v.FlowNode_Id != null)
                                .OrderByDescending(v => v.Id)
                                .Select(v => v.FlowNode_Id)
                                .FirstOrDefaultAsync();

                            if (prev_note_id == null)
                                throw new MsgException("没有可反审核的节点!");



                            /*说明是会签/*/
                            if (prev_note_id == flowInstance.CurrNode_Id)
                            {

                            }

                            throw new MsgException("未实现");
                        }
                    //break;
                    default:
                        break;
                }


                await flowInstances.UpdateAsync(flowInstance);
                bill_Entity.FlowStatus = flowInstance.Status;
                await loader.GetService<IRepository<TBillEntity>>().UpdateAsync(bill_Entity);

                /*生成审批事件[审批中]*/
                await loader.GetService<IEventBus>().TriggerEventAsync(new FlowOperateing<TBillEntity>(bill_Entity, flowInstance, input));

                /*注册审核事件[审批后]*/
                var msg = new FlowOperated<TBillEntity>(bill_Entity.Id);
                flowInstances.AddCommitEventListen(msg.TriggerEvent);

                /*提交事务*/
                //if (auto_tran)
                //    await flowInstances.CommmitAsync();

                return flowInstance;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await lockHolder.LockRelease();
            }
        }

        /// <summary>
        /// 获取指定节点已审核的人
        /// </summary>
        /// <param name="flow_instance_id">流程实例ID</param>
        /// <param name="note_id">指定的节点ID</param>
        /// <param name="can_check_user_ids">可审核的人</param>
        /// <returns></returns>
        private async Task<IEnumerable<FlowStep>> GetNoteCheckeSteps(string flow_instance_id, string note_id, string[] can_check_user_ids)
        {
            var flowSteps = loader.GetService<IRepository<FlowStep>>();

            /*取最近一次的提交*/
            var last_submit_id = await flowSteps
                .Where(v => v.FlowInstance_Id == flow_instance_id && v.Action == FlowActionEnum.submit && v.IsFinished)
                .OrderByDescending(v => v.Id)
                .Select(v => v.Id)
                .FirstOrDefaultAsync();

            /*取该节点哪些人已经审核了,且没有反审核的*/
            var checked_steps = await flowSteps
                .Where(v => v.FlowInstance_Id == flow_instance_id &&
                            v.FlowNode_Id == note_id &&
                            v.IsFinished &&
                            v.Id.CompareTo(last_submit_id) > 0 &&
                            can_check_user_ids.Contains(v.Operater_Id) &&
                            /*审批动作=通过/退回/拒绝*/
                            (v.Action == FlowActionEnum.pass || v.Action == FlowActionEnum.ng) &&
                            /*没有反审核的*/
                            !flowSteps.Any(x => x.FlowInstance_Id == v.FlowInstance_Id &&
                                                x.FlowNode_Id == v.FlowNode_Id &&
                                                x.IsFinished &&
                                                x.Id.CompareTo(last_submit_id) > 0 &&
                                                x.Action == FlowActionEnum.uncheck &&
                                                v.Operater_Id == v.Operater_Id))
                .OrderByDescending(v => v.Id)
                .ToArrayAsync();

            return checked_steps;
        }

        /// <summary>
        /// 获取余下的步骤
        /// </summary>
        /// <typeparam name="TBillEntity">类型</typeparam>
        /// <param name="bill_Entity">单据实例</param>
        /// <param name="work_flow_id">流程ID</param>
        /// <param name="curr_note_id">当前节点</param>
        /// <returns></returns>
        private async Task<FlowNodeModel[]> MatchNextFlowNotes<TBillEntity>(TBillEntity bill_Entity, string work_flow_id, string curr_note_id) where TBillEntity : class, IHaveCheck, new()
        {
            IEnumerable<FlowNodeModel> flowNodeModels = Array.Empty<FlowNodeModel>();
            if (work_flow_id != null)
                flowNodeModels = await loader.GetService<IEventBus>().RequestAsync<FlowNodeModel[], string>(work_flow_id);

            /*匹配到的流程*/
            var match_nodes = MatchNodes(flowNodeModels, bill_Entity, curr_note_id).ToArray();
            return match_nodes;
        }

        /// <summary>
        /// 写入新的步骤
        /// </summary>
        /// <typeparam name="TBillEntity">实体类型</typeparam>
        /// <param name="bill_Entity">单据实例</param>
        /// <param name="flowInstance">流程实例</param>
        /// <param name="match_nodes">要写入的节点</param>
        /// <param name="nextCheckerIds">指定的下级审核人</param>
        /// <param name="excludeUserIds">要排除的审核人</param>
        /// <returns></returns>
        private async Task WriteNextStep<TBillEntity>(TBillEntity bill_Entity,
                                                      FlowInstance flowInstance,
                                                      FlowNodeModel[] match_nodes,
                                                      string[] nextCheckerIds,
                                                      string[] excludeUserIds)
            where TBillEntity : class, IHaveCheck, new()
        {
            var flowSteps = loader.GetService<IRepository<FlowStep>>();
            var flowStepCheckers = loader.GetService<IRepository<FlowStepChecker>>();
            for (int i = 0; i < match_nodes.Length; i++)
            {
                var item = match_nodes[i];

                switch (item.NodeEnum)
                {
                    case FlowNodeEnum.start:
                    case FlowNodeEnum.end:
                    case FlowNodeEnum.branch:
                    case FlowNodeEnum.branch_child:
                    case FlowNodeEnum.cond:
                    case FlowNodeEnum.cc:
                        break;
                    case FlowNodeEnum.check:
                        /*计算可审批人*/
                        var checkerIds = await MatchCheckerIds(item, bill_Entity, flowInstance.Sponsor_Id).ToListAsync();

                        /*写入待完成步骤*/
                        var step = await flowSteps.AddAsync(new FlowStep
                        {
                            Action = null,
                            Desc = null,
                            FlowInstance_Id = flowInstance.Id,
                            FlowNodeName = item.Title,
                            FlowNode_Id = item.Id,
                            Id = null,
                            Operater_Id = null,
                            OperaterName = null,
                            OperateTime = null,
                            BeForm_Id = null,
                            IsFinished = false,
                        });


                        /*指定下级审核人*/
                        if (i == 0 && nextCheckerIds != null && item.Checkers.Any(v => v.CheckerEnum == FlowNodeCheckerEnum.prev_appoint))
                            checkerIds.AddRange(nextCheckerIds);

                        /*要排除指定某些人*/
                        if (i == 0 && excludeUserIds != null && item.Checkers.Any(v => v.CheckerEnum == FlowNodeCheckerEnum.prev_appoint))
                            checkerIds = checkerIds.Where(v => !excludeUserIds.Contains(v)).ToList();

                        /*写入待完成的审核人*/
                        var ids = checkerIds.Where(v => !v.IsNullOrWhiteSpace()).Distinct().ToArray();
                        foreach (var id in ids)
                        {
                            await flowStepCheckers.AddAsync(new FlowStepChecker
                            {
                                Bill_Id = bill_Entity.Id,
                                Id = null,
                                FlowStep_Id = step.Id,
                                FlowInstance_Id = flowInstance.Id,
                                User_Id = id
                            });
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 设置当前节点
        /// </summary>
        /// <param name="flowInstance"></param>
        /// <param name="note"></param>
        private static void SetCurrNote(FlowInstance flowInstance, FlowNodeModel note)
        {
            flowInstance.CurrNode_Id = note?.Id;
            flowInstance.CurrNodeName = note?.Title;
        }

        /// <summary>
        /// 设置流程结束
        /// </summary>
        /// <param name="flowInstance"></param>
        /// <param name="statusEnum"></param>
        private static void SetFlowCompalte(FlowInstance flowInstance, FlowStatusEnum statusEnum)
        {
            flowInstance.IsComlete = true;
            flowInstance.CompleteTime = DateTime.Now;
            flowInstance.Status = statusEnum;
            flowInstance.CurrNode_Id = null;
            flowInstance.CurrNodeName = null;
        }



        /// <summary>
        /// 获取可审核的人
        /// </summary>
        /// <typeparam name="TBillEntity">类型</typeparam>
        /// <param name="flowNode">节点</param>
        /// <param name="bill_Entity">单据实例</param>
        /// <param name="sponsor_id">提交人</param>
        /// <returns></returns>
        public async IAsyncEnumerable<string> MatchCheckerIds<TBillEntity>(FlowNodeModel flowNode, TBillEntity bill_Entity, string sponsor_id)
        {
            if (flowNode.Checkers == null)
                yield break;

            foreach (var item in flowNode.Checkers)
            {
                switch (item.CheckerEnum)
                {
                    case FlowNodeCheckerEnum.user:
                        {
                            if (item.Checker_Id.IsNullOrWhiteSpace())
                                continue;

                            yield return item.Checker_Id;
                            break;
                        }
                    case FlowNodeCheckerEnum.role:
                        {
                            if (item.Checker_Id.IsNullOrWhiteSpace())
                                continue;

                            var list = await loader
                               .GetService<IRepository<RoleMember>>()
                               .Where(v => v.FKey_Id == item.Checker_Id)
                               .Select(v => v.Value_Id)
                               .ToListAsync();

                            foreach (var uid in list)
                                yield return uid;

                            break;
                        }
                    case FlowNodeCheckerEnum.field:
                        {
                            if (item.Checker_Id.IsNullOrWhiteSpace())
                                continue;

                            var value = bill_Entity.GetValue(item.Checker_Id)?.ToString();
                            if (!value.IsNullOrWhiteSpace())
                                yield return value;
                            break;
                        }
                    case FlowNodeCheckerEnum.dept:
                        {
                            if (item.Checker_Id.IsNullOrWhiteSpace())
                                continue;

                            var list = await loader
                                .GetService<IRepository<DeptMember>>()
                                .Where(v => v.Dept_Id == item.Checker_Id)
                                .Select(v => v.User_Id)
                                .ToListAsync();

                            foreach (var uid in list)
                                yield return uid;

                            break;
                        }
                    case FlowNodeCheckerEnum.dept_manage:
                        {
                            if (item.Checker_Id.IsNullOrWhiteSpace())
                                continue;

                            var list = await loader
                                .GetService<IRepository<DeptMember>>()
                                .Where(v => v.Dept_Id == item.Checker_Id && v.IsManager)
                                .Select(v => v.User_Id)
                                .ToListAsync();

                            foreach (var uid in list)
                                yield return uid;

                            break;
                        }
                    case FlowNodeCheckerEnum.parent_dept:
                        {
                            var deptMembers = loader
                                .GetService<IRepository<DeptMember>>()
                                .Where(v => v.User_Id == sponsor_id);
                            var depts = loader
                                .GetService<IRepository<Dept>>()
                                .Where(v => deptMembers.Any(x => x.Dept_Id == v.Id));
                            var list = await loader
                                .GetService<IRepository<DeptMember>>()
                                .Where(v => depts.Any(x => x.Super_Id == v.Dept_Id))
                                .Select(v => v.User_Id)
                                .ToListAsync();

                            foreach (var uid in list)
                                yield return uid;

                            break;
                        }
                    case FlowNodeCheckerEnum.parent_dept_manage:
                        {
                            var deptMembers = loader
                                .GetService<IRepository<DeptMember>>()
                                .Where(v => v.User_Id == sponsor_id);
                            var depts = loader
                                .GetService<IRepository<Dept>>()
                                .Where(v => deptMembers.Any(x => x.Dept_Id == v.Id));
                            var list = await loader
                                .GetService<IRepository<DeptMember>>()
                                .Where(v => v.IsManager && depts.Any(x => x.Super_Id == v.Dept_Id))
                                .Select(v => v.User_Id)
                                .ToListAsync();

                            foreach (var uid in list)
                                yield return uid;

                            break;
                        }
                    case FlowNodeCheckerEnum.prev_appoint:
                        break;
                    case FlowNodeCheckerEnum.cur_dept_manage:
                        {
                            var deptMembers = loader
                                .GetService<IRepository<DeptMember>>()
                                .Where(v => v.User_Id == sponsor_id);

                            var list = await loader
                                .GetService<IRepository<DeptMember>>()
                                .Where(v => v.IsManager && deptMembers.Any(x => x.Dept_Id == v.Dept_Id))
                                .Select(v => v.User_Id)
                                .ToListAsync();

                            foreach (var uid in list)
                                yield return uid;

                            break;
                        }
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 计算流程节点
        /// </summary>
        /// <typeparam name="TBillEntity">单据类型</typeparam>
        /// <param name="nodes">所有流程节点</param>
        /// <param name="bill_Entity">单据实例</param>
        /// <param name="start_node_id">开始节点,提交时为Null,审核时为当前流程节点</param>
        /// <returns></returns>
        private static IEnumerable<FlowNodeModel> MatchNodes<TBillEntity>(IEnumerable<FlowNodeModel> nodes, TBillEntity bill_Entity, string start_node_id)
            where TBillEntity : class, IHaveCheck, new()
        {
            if (nodes == null || !nodes.Any())
                yield break;

            foreach (var item in nodes)
            {
                if (item.Id == start_node_id)
                    start_node_id = null;

                switch (item.NodeEnum)
                {
                    case FlowNodeEnum.check:
                    case FlowNodeEnum.cc:
                        if (start_node_id.IsNullOrWhiteSpace() || item.Id == start_node_id)
                            yield return item;
                        break;
                    case FlowNodeEnum.branch:
                        /*确定走哪个分支*/
                        var child_brahchs = item
                            .Nodes
                            .OrderByDescending(v => v.IsDefault == true ? -1 : Math.Max(v.Weight ?? 0, 0))
                            .ThenBy(v => v.OrderVal);
                        var branch = child_brahchs.FirstOrDefault(v => MatchBrahsh(v, bill_Entity));
                        var children = MatchNodes(branch.Nodes, bill_Entity, start_node_id);
                        foreach (var child in children)
                            yield return child;
                        break;
                    case FlowNodeEnum.start:
                    case FlowNodeEnum.end:
                    //if (start_node_id.IsNullOrWhiteSpace() || item.Id == start_node_id)
                    //    yield return item;
                    //break;
                    case FlowNodeEnum.branch_child:
                    case FlowNodeEnum.cond:
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 判断子分支是否满足
        /// </summary>
        /// <typeparam name="TBillEntity">类型</typeparam>
        /// <param name="flowNode">分支节点</param>
        /// <param name="bill_Entity">实体</param>
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
}
