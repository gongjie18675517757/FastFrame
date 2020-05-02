using FastFrame.Entity;
using FastFrame.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Repository;
using FastFrame.Infrastructure;
using FastFrame.Entity.Flow;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Entity.Basis;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 流程中心
    /// </summary>
    public partial class FlowCenterService : IService
    {
        private readonly IServiceProvider serviceProvider;

        public FlowCenterService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private IRepository<TEntity> LoadRepository<TEntity>() where TEntity : class, IEntity
                => serviceProvider.GetService<IRepository<TEntity>>();

        private IAppSessionProvider AppSession
            => serviceProvider.GetService<IAppSessionProvider>();

        /// <summary>
        /// 流程操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="flowOperate"></param>
        /// <returns></returns>
        public async Task<FlowOperateOutput> HandleFlowOperateAsync<TEntity>(string id, FlowOperateInput flowOperate) where TEntity : class, IHaveCheck
        {
            var entityRepisitory = LoadRepository<TEntity>();
            var entity = await entityRepisitory.GetAsync(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return await HandleFlowOperateAsync(entity, flowOperate);
        }

        /// <summary>
        /// 流程操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowOperate"></param>
        /// <returns></returns>
        public async Task<FlowOperateOutput> HandleFlowOperateAsync<TEntity>(TEntity entity, FlowOperateInput flowOperate) where TEntity : class, IHaveCheck
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (flowOperate is null)
            {
                throw new ArgumentNullException(nameof(flowOperate));
            }

            var eventBus = serviceProvider.GetService<IEventBus>();
            var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            await eventBus.TriggerEventAsync(new FlowOperateBefore<TEntity>(entity, flowOperate));
            FlowOperateOutput handleResult = flowOperate.ActionEnum switch
            {
                FlowActionEnum.submit => await HandleSubmitActionAsync(entity, flowOperate),
                FlowActionEnum.unsubmit => await HandleUnSubmitActionAsync(entity, flowOperate),
                FlowActionEnum.pass => await HandlePassActionAsync(entity, flowOperate),
                FlowActionEnum.withdraw => await HandleWithdrawActionAsync(entity, flowOperate),
                FlowActionEnum.refuse => await HandleRefuseActionAsync(entity, flowOperate),
                FlowActionEnum.uncheck => await HandleUnCheckActionAsync(entity, flowOperate),
                _ => throw new NotImplementedException(),
            };
            entity.FlowStatus = handleResult.FlowStatus;
            var entityRepisitory = LoadRepository<TEntity>();
            await entityRepisitory.UpdateAsync(entity);

            await eventBus.TriggerEventAsync(new FlowOperateing<TEntity>(entity, flowOperate, handleResult.FlowProcesses));
            await unitOfWork.CommmitAsync();
            await eventBus.TriggerEventAsync(new FlowOperated<TEntity>(entity, flowOperate, handleResult.FlowProcesses));

            return handleResult;
        }

        /// <summary>
        /// 验证当前步骤是否可以操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> VerificationCanCheck(string id)
        {
            var flowInstances = LoadRepository<FlowInstance>();
            var flowStepUsers = LoadRepository<FlowStepUser>();
            var currUser = AppSession.CurrUser;


            return currUser.IsAdmin ||
                        await flowStepUsers.AnyAsync(v =>
                                flowInstances.Any(r =>
                                        r.CurrStep_Id == v.FlowStep_Id && r.Bill_Id == id && r.Enabled == EnabledMark.enabled));
        }

        /// <summary>
        /// 处理反审核操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowOperate"></param>
        /// <returns></returns>
        public async Task<FlowOperateOutput> HandleUnCheckActionAsync<TEntity>(TEntity entity, FlowOperateInput flowOperate) where TEntity : class, IHaveCheck
        {
            var flowInstances = LoadRepository<FlowInstance>();
            var flowProcesses = LoadRepository<FlowProcess>();
            var flowSteps = LoadRepository<FlowStep>();
            var currUser = AppSession.CurrUser;

            var flowInstance = await flowInstances
                .Where(v => v.Bill_Id == entity.Id)
                .OrderByDescending(v => v.Version)
                .FirstOrDefaultAsync();

            if (flowInstance == null)
                throw new MsgException("流程未提交");

            /*最后一次操作*/
            var lastProcess = await flowProcesses
                    .Where(v => v.FlowInstance_Id == flowInstance.Id && v.NodeKey != -1 && v.NodeKey != 0)
                    .OrderByDescending(v => v.Id)
                    .FirstOrDefaultAsync();

            if (lastProcess == null || lastProcess.Operater_Id != currUser.Id)
                throw new MsgException("只能反审核流程的最后一步，当前流程的最后一步不是由您审核的");



            flowInstance.Enabled = EnabledMark.enabled;
            flowInstance.Status = FlowStatusEnum.processing;
            flowInstance.IsComlete = false;
            flowInstance.LastCheckTime = DateTime.Now;
            flowInstance.LastChecker_Id = currUser.Id;
            flowInstance.CompleteTime = null;
            flowInstance.CurrStep_Id = lastProcess.FlowStep_Id;

            await flowInstances.UpdateAsync(flowInstance);

            var flowProcess = await flowProcesses.AddAsync(new FlowProcess
            {
                Action = flowOperate.ActionEnum,
                Desc = flowOperate.Desc,
                FlowInstance_Id = flowInstance.Id,
                FlowStepName = lastProcess.FlowStepName,
                Id = null,
                FlowStep_Id = lastProcess.FlowStep_Id,
                NodeKey = lastProcess.NodeKey,
                OperaterName = currUser.Name,
                Operater_Id = currUser.Id,
                OperateTime = DateTime.Now
            });

            return new FlowOperateOutput
            {
                FlowStatus = flowInstance.Status,
                FlowProcesses = new[] { flowProcess }
            };
        }

        /// <summary>
        /// 处理拒绝动作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowOperate"></param>
        /// <returns></returns>
        public async Task<FlowOperateOutput> HandleRefuseActionAsync<TEntity>(TEntity entity, FlowOperateInput flowOperate) where TEntity : class, IHaveCheck
        {
            var flowInstances = LoadRepository<FlowInstance>();
            var flowProcesses = LoadRepository<FlowProcess>();
            var flowSteps = LoadRepository<FlowStep>();
            var currUser = AppSession.CurrUser;

            var flowInstance = await flowInstances
                .Where(v => v.Bill_Id == entity.Id && v.Enabled == EnabledMark.enabled)
                .OrderByDescending(v => v.Version)
                .FirstOrDefaultAsync();

            if (flowInstance == null)
                throw new MsgException("流程未提交");

            if (!await VerificationCanCheck(entity.Id))
                throw new MsgException("你没有权限处理当前步骤！");

            flowInstance.IsComlete = true;
            flowInstance.CompleteTime = DateTime.Now;
            flowInstance.CurrStep_Id = null;
            flowInstance.Enabled = EnabledMark.disabled;
            flowInstance.Status = FlowStatusEnum.refuse;
            await flowInstances.UpdateAsync(flowInstance);

            var currStep = await flowSteps
                   .Where(v => v.Id == flowInstance.CurrStep_Id)
                   .Select(v => new { v.Id, v.StepName, v.FlowNodeKey })
                   .FirstOrDefaultAsync();

            var lastStep = await flowSteps
                  .Where(v => v.FlowInstance_Id == flowInstance.Id && v.FlowNodeKey == -1)
                  .Select(v => new { v.Id, v.StepName, v.FlowNodeKey })
                  .FirstOrDefaultAsync();

            return new FlowOperateOutput
            {
                FlowStatus = FlowStatusEnum.refuse,
                FlowProcesses = new[] {
                    await flowProcesses.AddAsync(new FlowProcess
                        {
                            Action = flowOperate.ActionEnum,
                            Desc = flowOperate.Desc,
                            FlowInstance_Id = flowInstance.Id,
                            FlowStep_Id = currStep.Id,
                            FlowStepName = currStep.StepName,
                            Id = null,
                            OperaterName = currUser.Name,
                            Operater_Id = currUser.Id,
                            OperateTime = DateTime.Now,
                            NodeKey = currStep.FlowNodeKey
                        }),
                    await flowProcesses.AddAsync(new FlowProcess
                        {
                            Action = flowOperate.ActionEnum,
                            Desc = "已完结",
                            FlowInstance_Id = flowInstance.Id,
                            FlowStep_Id = lastStep.Id,
                            FlowStepName = lastStep.StepName,
                            Id = null,
                            OperaterName = currUser.Name,
                            Operater_Id = currUser.Id,
                            OperateTime = DateTime.Now,
                            NodeKey = lastStep.FlowNodeKey
                        }),
                },
            };
        }

        /// <summary>
        /// 处理退回动作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowOperate"></param>
        /// <returns></returns>
        public async Task<FlowOperateOutput> HandleWithdrawActionAsync<TEntity>(TEntity entity, FlowOperateInput flowOperate) where TEntity : class, IHaveCheck
        {
            var flowInstances = LoadRepository<FlowInstance>();
            var flowProcesses = LoadRepository<FlowProcess>();
            var flowSteps = LoadRepository<FlowStep>();
            var currUser = AppSession.CurrUser;

            var flowInstance = await flowInstances
                .Where(v => v.Bill_Id == entity.Id && v.Enabled == EnabledMark.enabled)
                .OrderByDescending(v => v.Version)
                .FirstOrDefaultAsync();

            if (flowInstance == null)
                throw new MsgException("流程未提交");

            if (!await VerificationCanCheck(entity.Id))
                throw new MsgException("你没有权限处理当前步骤！");

            flowInstance.IsComlete = true;
            flowInstance.CompleteTime = DateTime.Now;
            flowInstance.CurrStep_Id = null;
            flowInstance.Enabled = EnabledMark.disabled;
            flowInstance.Status = FlowStatusEnum.returned;
            await flowInstances.UpdateAsync(flowInstance);

            var currStep = await flowSteps
                   .Where(v => v.Id == flowInstance.CurrStep_Id)
                   .Select(v => new { v.Id, v.StepName, v.FlowNodeKey })
                   .FirstOrDefaultAsync();

            var lastStep = await flowSteps
                  .Where(v => v.FlowInstance_Id == flowInstance.Id && v.FlowNodeKey == -1)
                  .Select(v => new { v.Id, v.StepName, v.FlowNodeKey })
                  .FirstOrDefaultAsync();

            return new FlowOperateOutput
            {
                FlowStatus = flowInstance.Status,
                FlowProcesses = new[] {
                    await flowProcesses.AddAsync(new FlowProcess
                        {
                            Action = flowOperate.ActionEnum,
                            Desc = flowOperate.Desc,
                            FlowInstance_Id = flowInstance.Id,
                            FlowStep_Id = currStep.Id,
                            FlowStepName = currStep.StepName,
                            Id = null,
                            OperaterName = currUser.Name,
                            Operater_Id = currUser.Id,
                            OperateTime = DateTime.Now,
                            NodeKey = currStep.FlowNodeKey
                        }),
                    await flowProcesses.AddAsync(new FlowProcess
                        {
                            Action = flowOperate.ActionEnum,
                            Desc = "已完结",
                            FlowInstance_Id = flowInstance.Id,
                            FlowStep_Id = lastStep.Id,
                            FlowStepName = lastStep.StepName,
                            Id = null,
                            OperaterName = currUser.Name,
                            Operater_Id = currUser.Id,
                            OperateTime = DateTime.Now,
                            NodeKey = lastStep.FlowNodeKey
                        }),
                },
            };
        }


        /// <summary>
        /// 处理审核通过动作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowOperate"></param>
        /// <returns></returns>
        public async Task<FlowOperateOutput> HandlePassActionAsync<TEntity>(TEntity entity, FlowOperateInput flowOperate) where TEntity : class, IHaveCheck
        {
            var flowInstances = LoadRepository<FlowInstance>();
            var flowProcesses = LoadRepository<FlowProcess>();
            var flowSteps = LoadRepository<FlowStep>();
            var currUser = AppSession.CurrUser;

            var flowOperateOutput = new FlowOperateOutput();

            var flowInstance = await flowInstances
                .Where(v => v.Bill_Id == entity.Id && v.Enabled == EnabledMark.enabled)
                .OrderByDescending(v => v.Version)
                .FirstOrDefaultAsync();

            if (flowInstance == null)
                throw new MsgException("流程未提交");

            if (!await VerificationCanCheck(entity.Id))
                throw new MsgException("你没有权限处理当前步骤！");

            var currStep = await flowSteps
                        .Where(v => v.Id == flowInstance.CurrStep_Id)
                        .Select(v => new
                        {
                            v.FlowNodeKey,
                            v.NextStepKey,
                            v.NextStep_Id,
                            v.StepName,
                            v.Id
                        })
                        .FirstOrDefaultAsync();


            if (currStep == null)
                throw new MsgException("获取审批步骤失败！");


            var flowProcess = await flowProcesses.AddAsync(new FlowProcess
            {
                Action = FlowActionEnum.pass,
                Desc = flowOperate.Desc,
                FlowInstance_Id = flowInstance.Id,
                FlowStep_Id = currStep.Id,
                FlowStepName = currStep.StepName,
                Id = null,
                OperaterName = currUser.Name,
                Operater_Id = currUser.Id,
                OperateTime = DateTime.Now,
                NodeKey = currStep.FlowNodeKey
            });

            flowInstance.LastChecker_Id = currUser.Id;
            flowInstance.LastCheckTime = DateTime.Now;

            /*当到达最后一步时*/
            if (currStep.NextStepKey == -1)
            {
                flowInstance.CurrStep_Id = null;
                flowInstance.CompleteTime = DateTime.Now;
                flowInstance.IsComlete = true;
                flowInstance.Status = FlowStatusEnum.finished;
                await flowInstances.UpdateAsync(flowInstance);

                var nextStep = await flowSteps
                        .Where(v => v.Id == currStep.NextStep_Id)
                        .Select(v => new
                        {
                            v.StepName,
                            v.Id
                        })
                        .FirstOrDefaultAsync();

                var lastFlowProcess = await flowProcesses.AddAsync(new FlowProcess
                {
                    Action = FlowActionEnum.pass,
                    Desc = "审批完结",
                    FlowInstance_Id = flowInstance.Id,
                    FlowStep_Id = nextStep.Id,
                    FlowStepName = nextStep.StepName,
                    Id = null,
                    OperaterName = currUser.Name,
                    Operater_Id = currUser.Id,
                    OperateTime = DateTime.Now,
                    NodeKey = currStep.FlowNodeKey
                });

                return new FlowOperateOutput
                {
                    FlowStatus = FlowStatusEnum.returned,
                    FlowProcesses = new[] { flowProcess, lastFlowProcess }
                };
            }
            else
            {
                flowInstance.CurrStep_Id = currStep.NextStep_Id;
                await flowInstances.UpdateAsync(flowInstance);
                return new FlowOperateOutput
                {
                    FlowStatus = FlowStatusEnum.processing,
                    FlowProcesses = new[] { flowProcess }
                };
            }
        }


        /// <summary>
        /// 处理撤回动作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowOperate"></param>
        /// <returns></returns>
        public async Task<FlowOperateOutput> HandleUnSubmitActionAsync<TEntity>(TEntity entity, FlowOperateInput flowOperate) where TEntity : class, IHaveCheck
        {
            var flowInstances = LoadRepository<FlowInstance>();
            var flowProcesses = LoadRepository<FlowProcess>();
            var flowSteps = LoadRepository<FlowStep>();
            var currUser = AppSession.CurrUser;

            var flowInstance = await flowInstances
                .Where(v => v.Bill_Id == entity.Id && v.Enabled == EnabledMark.enabled)
                .OrderByDescending(v => v.Version)
                .FirstOrDefaultAsync();

            if (flowInstance == null)
                throw new MsgException("流程未提交");

            //if (entity.FlowStatus != FlowStatusEnum.processing)
            //    throw new MsgException("流程已完结，不可撤回！");

            flowInstance.IsComlete = true;
            flowInstance.CompleteTime = DateTime.Now;
            flowInstance.CurrStep_Id = null;
            flowInstance.Enabled = EnabledMark.disabled;
            flowInstance.Status = FlowStatusEnum.unsubmitted;
            await flowInstances.UpdateAsync(flowInstance);

            var lastStep = await flowSteps
                  .Where(v => v.FlowInstance_Id == flowInstance.Id && v.FlowNodeKey == -1)
                  .Select(v => new { v.Id, v.StepName })
                  .FirstOrDefaultAsync();

            var flowProcess = await flowProcesses.AddAsync(new FlowProcess
            {
                Action = FlowActionEnum.unsubmit,
                Desc = flowOperate.Desc,
                FlowInstance_Id = flowInstance.Id,
                FlowStep_Id = lastStep.Id,
                FlowStepName = lastStep.StepName,
                Id = null,
                OperaterName = currUser.Name,
                Operater_Id = currUser.Id,
                OperateTime = DateTime.Now,
                NodeKey = -1
            });

            return new FlowOperateOutput
            {
                FlowProcesses = new[] { flowProcess },
                FlowStatus = FlowStatusEnum.unsubmitted
            };
        }

        /// <summary>
        /// 处理提交动作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<FlowOperateOutput> HandleSubmitActionAsync<TEntity>(TEntity entity, FlowOperateInput flowOperate) where TEntity : class, IHaveCheck
        {
            var currUser = AppSession.CurrUser;
            var flowInstances = LoadRepository<FlowInstance>();
            var flowProcesses = LoadRepository<FlowProcess>();
            var flowInstanceDepts = LoadRepository<FlowInstanceDept>();
            var flowSteps = LoadRepository<FlowStep>();
            var flowStepUsers = LoadRepository<FlowStepUser>();
            var flowSnapshots = LoadRepository<FlowSnapshot>();
            var workFlows = LoadRepository<WorkFlow>();
            var eventBuses = serviceProvider.GetService<IEventBus>();

            /*1,判断之前是否有提交过,有则判断是否结束*/
            var beforeInstance = await flowInstances
                    .Where(v => v.Bill_Id == entity.Id)
                    .OrderByDescending(v => v.Version)
                    .Select(v => new { v.Version, v.Enabled })
                    .FirstOrDefaultAsync();

            if (entity.FlowStatus != FlowStatusEnum.unsubmitted || entity.FlowStatus != FlowStatusEnum.returned)
                throw new MsgException("只有未提交/已退回的单据，才可重新提交！");

            if (beforeInstance != null && beforeInstance.Enabled == EnabledMark.enabled)
                throw new MsgException("流程还没结束，不可重新提交！");


            var type = typeof(TEntity);
            var beModuleText = serviceProvider.GetService<IDescriptionProvider>().GetClassDescription(type);

            /*2，取模块对应的流程ID*/
            var flowId = await workFlows
                            .Where(v => v.BeModule == type.Name && v.Enabled == EnabledMark.enabled)
                            .OrderByDescending(v => v.Version)
                            .Select(v => v.Id)
                            .FirstOrDefaultAsync();

            if (flowId.IsNullOrWhiteSpace())
                throw new MsgException("未配置流程");

            /*3,创建一个新的流程实例*/
            var flowInstance = new FlowInstance
            {
                BillNumber = null,
                BeModuleName = type.Name,
                BeModuleText = beModuleText,
                Bill_Id = entity.Id,
                Id = null,
                CompleteTime = null,
                CurrStep_Id = null,
                IsComlete = false,
                LastChecker_Id = null,
                LastCheckTime = null,
                Sponsor_Id = currUser?.Id,
                StartTime = DateTime.Now,
                Status = FlowStatusEnum.submitted,
                WorkFlow_Id = flowId,
                Enabled = EnabledMark.enabled,
                Version = beforeInstance?.Version ?? 0 + 1
            };

            if (entity is IHaveNumber numberEntity)
                flowInstance.BillNumber = numberEntity.Number;

            if (entity is IHaveDept haveDept)
                foreach (var deptId in haveDept.GetBeDeptIds())
                    await flowInstanceDepts.AddAsync(new FlowInstanceDept { BeDept_Id = deptId, FlowInstance_Id = flowInstance.Id });

            await flowInstances.AddAsync(flowInstance);

            /*4，取出流程配置并保存快照*/
            var workFlowDto = await eventBuses.RequestAsync<WorkFlowDto, string>(flowId);
            await flowSnapshots.AddAsync(new FlowSnapshot
            {
                FlowInstance_Id = flowInstance.Id,
                FKey_Id = flowId,
                SnapshotContent = workFlowDto.ToJson()
            });

            /*5，缓存单据快照*/
            await flowSnapshots.AddAsync(new FlowSnapshot
            {
                FlowInstance_Id = flowInstance.Id,
                FKey_Id = entity.Id,
                SnapshotContent = entity.ToJson()
            });


            /*6，计算步骤与审核人*/
            var flowNodes = workFlowDto.Nodes;
            var flowLines = workFlowDto.Lines;
            List<FlowNodeDto> stepNodes = GenerateFlowSteps(entity, flowNodes, flowLines);


            /*7, 插入流程步骤*/
            var stepList = new List<FlowStep>();
            for (int i = 0; i < stepNodes.Count; i++)
            {
                var stepNode = stepNodes[i];
                var flowStep = await flowSteps.AddAsync(new FlowStep
                {
                    FlowInstance_Id = flowInstance.Id,
                    FlowNode_Id = stepNode.Id,
                    PrevStep_Id = null,
                    NextStep_Id = null,
                    StepOrder = i + 1,
                    StepName = stepNode.Name,
                    FlowNodeKey = stepNode.Key,
                    PrevStepKey = 0,
                    NextStepKey = -1
                });
                stepList.Add(flowStep);

                /*匹配可审核的人（起点和终点不需要）*/
                if (stepNode.Key == 0 || stepNode.Key == -1)
                    continue;

                var stepUserList = await MatchStepCheckUser(entity, stepNode);
                foreach (var stepUser in stepUserList)
                {
                    stepUser.FlowInstance_Id = flowInstance.Id;
                    stepUser.FlowStep_Id = flowStep.Id;

                    await flowStepUsers.AddAsync(stepUser);
                }
            }

            for (int i = 1; i < stepList.Count - 1; i++)
            {
                var currStep = stepList[i];
                var prevStep = stepList[i - 1];
                var nextStep = stepList[i + 1];

                currStep.PrevStep_Id = prevStep.Id;
                currStep.PrevStepKey = prevStep.FlowNodeKey;
                currStep.NextStep_Id = nextStep.Id;
                currStep.NextStepKey = nextStep.FlowNodeKey;
            }

            /*8,插入审核过程*/
            var startStep = stepList[0];
            var flowProcess = await flowProcesses.AddAsync(new FlowProcess
            {
                Id = null,
                FlowStep_Id = startStep.Id,
                Action = FlowActionEnum.submit,
                Desc = flowOperate.Desc,
                FlowInstance_Id = flowInstance.Id,
                FlowStepName = startStep.StepName,
                OperateTime = DateTime.Now,
                Operater_Id = currUser.Id,
                OperaterName = currUser.Name,
                NodeKey = 0
            });

            flowInstance.CurrStep_Id = stepList[1].Id;

            return new FlowOperateOutput
            {
                FlowStatus = FlowStatusEnum.submitted,
                FlowProcesses = new[] { flowProcess }
            };
        }

        /// <summary>
        /// 匹配流程步骤可审核的人
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowNode"></param>
        /// <returns></returns>
        private async Task<IEnumerable<FlowStepUser>> MatchStepCheckUser<TEntity>(TEntity entity, FlowNodeDto flowNode)
        {
            var stepUserList = new List<FlowStepUser>();
            var roleMembers = LoadRepository<RoleMember>();
            var deptMembers = LoadRepository<DeptMember>();
            var users = LoadRepository<User>();

            /*直接指定的人*/
            stepUserList.AddRange(flowNode.Users.Select(v => new FlowStepUser
            {
                FlowStep_Id = null,
                Id = null,
                BeDept_Id = null,
                BeRole_Id = null,
                User_Id = v.Id,
                FlowInstance_Id = null
            }));

            /*指定的角色的所有人*/
            var roleKeys = flowNode.Roles.Select(v => v.Id).ToArray();
            var roleUsers = await roleMembers
                    .Where(v => roleKeys.Contains(v.Id))
                    .Select(v => new { v.User_Id, v.Role_Id })
                    .ToListAsync();

            stepUserList.AddRange(roleUsers.Select(v => new FlowStepUser
            {
                FlowStep_Id = null,
                Id = null,
                BeDept_Id = null,
                BeRole_Id = v.Role_Id,
                User_Id = v.User_Id,
                FlowInstance_Id = null
            }));

            /*部门主管审核*/
            if (entity is IHaveDept haveDept && flowNode.DeptCheck)
            {
                var deptKeys = haveDept.GetBeDeptIds();
                var deptUsers = await deptMembers
                        .Where(v => v.IsManager && deptKeys.Contains(v.Dept_Id))
                        .Select(v => new { v.Dept_Id, v.User_Id })
                        .ToListAsync();

                stepUserList.AddRange(deptUsers.Select(v => new FlowStepUser
                {
                    FlowStep_Id = null,
                    Id = null,
                    BeDept_Id = v.Dept_Id,
                    BeRole_Id = null,
                    User_Id = v.User_Id,
                    FlowInstance_Id = null
                }));
            }

            /*动态字段*/
            var fieldValues = flowNode.Fields
                    .Select(v => entity.GetValue(v)?.ToString())
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .ToArray();

            var fieldUserIds = await users
                    .Where(v => fieldValues.Contains(v.Id))
                    .Select(v => v.Id)
                    .ToListAsync();

            stepUserList.AddRange(fieldUserIds.Select(v => new FlowStepUser
            {
                FlowStep_Id = null,
                Id = null,
                BeDept_Id = null,
                BeRole_Id = null,
                User_Id = v,
                FlowInstance_Id = null
            }));

            return stepUserList;
        }

        /// <summary>
        /// 生成流程步骤
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowNodes"></param>
        /// <param name="flowLines"></param>
        /// <returns></returns>
        private List<FlowNodeDto> GenerateFlowSteps<TEntity>(TEntity entity, IEnumerable<FlowNodeDto> flowNodes, IEnumerable<FlowLineDto> flowLines)
            where TEntity : class
        {
            var start = flowNodes.FirstOrDefault(v => v.Key == 0);
            var end = flowNodes.FirstOrDefault(v => v.Key == -1);
            var stepNodes = new List<FlowNodeDto>();

            if (start == null)
                throw new MsgException("流程没有配置起点");

            if (end == null)
                throw new MsgException("流程没有配置终点");

            stepNodes.Add(start);

            var currNodeKey = start.Key;
            var currNode = start;
            while (true)
            {
                var nextLines = flowLines.Where(v => v.From == currNodeKey);
                var nextLine = MatchNodeLines(entity, nextLines);

                if (nextLine == null)
                    throw new MsgException($"流程配置不正确，节点:{currNode.Name}无法匹配下到一步节点！");

                /*nodeKey为-1时，表示流程结束*/
                var nextNodeKey = nextLine.To;
                if (nextNodeKey == -1)
                    break;

                var nextNode = flowNodes.FirstOrDefault(v => v.Key == nextLine.To);
                stepNodes.Add(nextNode);
                currNodeKey = nextNodeKey;
                currNode = nextNode;
            }

            stepNodes.Add(end);
            return stepNodes;
        }

        /// <summary>
        /// 匹配流程线
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flowLines"></param>
        /// <returns></returns>
        private FlowLineDto MatchNodeLines<TEntity>(TEntity entity, IEnumerable<FlowLineDto> flowLines) where TEntity : class
        {
            var entities = new[] { entity }.AsQueryable();
            var filterLines = flowLines.Where(v =>
             {
                 return entities
                         .DynamicQuery(
                             null,
                             new List<KeyValuePair<string, List<Filter>>>() {
                                new KeyValuePair<string, List<Filter>>(
                                        "and",
                                        v.Conds.Select(r =>
                                            new Filter
                                            {
                                                Name=r.FieldName,
                                                Compare=r.Compare,
                                                Value=r.Compare
                                            })
                                        .ToList())
                             })
                         .Any();
             });

            return filterLines
                        .OrderByDescending(v => v.Weights)
                        .FirstOrDefault();
        }


    }
}
