using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using FastFrame.Entity.Basis;

namespace FastFrame.Application.Flow
{
    public partial class WorkFlowService : IRequestHandle<WorkFlowDto, string>
    {
        public async Task<int> GetLastVersion(string moduleName)
        {
            return await workFlowRepository.Where(v => v.BeModule == moduleName).Select(v => v.Version).OrderByDescending(v => v).FirstOrDefaultAsync() + 1;
        }

        public Task<WorkFlowDto> HandleRequestAsync(string request)
        {
            return GetAsync(request);
        }

        protected override async Task OnAdding(WorkFlowDto input, WorkFlow entity)
        {
            entity.Version = await GetLastVersion(entity.BeModule);

            await base.OnAdding(input, entity);
        }

        protected override async Task OnBeforeUpdate(WorkFlow before, WorkFlowDto after)
        {
            /*判断流程有没有被使用，有则不允许修改*/
            if (await Loader.GetService<IRepository<FlowInstance>>().AnyAsync(v => v.WorkFlow_Id == before.Id))
                throw new MsgException("流程使用中，不允许修改,可以复制当前流程并生成一个新的版本！");

            await base.OnBeforeUpdate(before, after);
        }

        protected override async Task OnAddOrUpdateing(WorkFlowDto input, WorkFlow entity)
        {
            /*验证流程节点是否配置正确*/
            VerifyFlowNodes(input);

            await base.OnAddOrUpdateing(input, entity);
        }

        protected override async Task OnChangeing(WorkFlowDto input, WorkFlow entity)
        {
            await base.OnChangeing(input, entity);

            var id = entity.Id;
            var flowNodes = Loader.GetService<IRepository<FlowNode>>();
            var afterNodeList = input?.Nodes.SelectLoopChild(v => v.Nodes);

            /*同步更新子节点*/
            await Loader
                .GetService<HandleOne2ManyService<FlowNode, FlowNode>>()
                .UpdateManyAsync(
                    v => v.WorkFlow_Id == id,
                    afterNodeList,
                    (a, b) => a.Id == b.Id,
                    v =>
                    {
                        v.WorkFlow_Id = entity.Id;
                        return v;
                    },
                    (before, after) =>
                    {
                        after.MapSet(before);
                        before.WorkFlow_Id = entity.Id;
                    }
                );

            input?.Nodes.EachLoopChild(v => v.Nodes, null, temp =>
            {
                var (patent, child) = temp;
                child.Super_Id = patent?.Id;
            });

            /*同步更新条件*/
            await Loader
                .GetService<HandleOne2ManyService<FlowNodeCond, FlowNodeCond>>()
                .UpdateManyAsync(
                    v => v.WorkFlow_Id == id,
                    afterNodeList?.SelectMany(v => v.Conds.Select(x =>
                    {
                        x.FlowNode_Id = v.Id;
                        x.WorkFlow_Id = id;
                        return x;
                    })),
                    (a, b) => a.Id == b.Id,
                    v =>
                    {
                        v.WorkFlow_Id = entity.Id;
                        return v;
                    },
                    (before, after) =>
                    {
                        after.MapSet(before);
                        before.WorkFlow_Id = entity.Id;
                    }
                );

            /*同步更新事件*/
            await Loader
                .GetService<HandleOne2ManyService<FlowNodeEvent, FlowNodeEvent>>()
                .UpdateManyAsync(
                    v => v.WorkFlow_Id == id,
                    afterNodeList?.SelectMany(v => v.Events.Select(x =>
                    {
                        x.FlowNode_Id = v.Id;
                        x.WorkFlow_Id = id;
                        return x;
                    })),
                    (a, b) => a.Id == b.Id,
                    v =>
                    {
                        v.WorkFlow_Id = entity.Id;
                        return v;
                    },
                    (before, after) =>
                    {
                        after.MapSet(before);
                        before.WorkFlow_Id = entity.Id;
                    }
                );

            /*同步更新审核人*/
            await Loader
                .GetService<HandleOne2ManyService<FlowNodeChecker, FlowNodeChecker>>()
                .UpdateManyAsync(
                    v => v.WorkFlow_Id == id,
                    afterNodeList?.SelectMany(v => v.Checkers.Select(x =>
                    {
                        x.FlowNode_Id = v.Id;
                        x.WorkFlow_Id = id;
                        return x;
                    })),
                    (a, b) => a.Id == b.Id,
                    v =>
                    {
                        v.WorkFlow_Id = entity.Id;
                        return v;
                    },
                    (before, after) =>
                    {
                        after.MapSet(before);
                        before.WorkFlow_Id = entity.Id;
                    }
                );
        }

        protected override async Task OnDeleteing(WorkFlow entity)
        {
            /*判断流程有没有被使用，有则不允许删除*/
            if (await Loader.GetService<IRepository<FlowInstance>>().AnyAsync(v => v.WorkFlow_Id == entity.Id))
                throw new MsgException("流程使用中，不允许删除,但是你可以禁用该流程！");

            await base.OnDeleteing(entity);
        }

        protected override async Task OnGeting(WorkFlowDto dto)
        {
            await base.OnGeting(dto);

            var id = dto.Id;
            var flowNodes = await Loader.GetService<IRepository<FlowNode>>().Where(v => v.WorkFlow_Id == id).MapTo<FlowNode, FlowNodeModel>().ToListAsync();
            var flowNodeConds = await Loader.GetService<IRepository<FlowNodeCond>>().Where(v => v.WorkFlow_Id == id).ToListAsync();
            var flowNodeEvents = await Loader.GetService<IRepository<FlowNodeEvent>>().Where(v => v.WorkFlow_Id == id).ToListAsync();
            var flowNodeCheckers = await Loader.GetService<IRepository<FlowNodeChecker>>().Where(v => v.WorkFlow_Id == id).ToListAsync();

            foreach (var node in flowNodes)
            {
                node.Conds = flowNodeConds.Where(v => v.FlowNode_Id == node.Id).ToList();
                node.Events = flowNodeEvents.Where(v => v.FlowNode_Id == node.Id).ToList();
                node.Checkers = flowNodeCheckers.Where(v => v.FlowNode_Id == node.Id).ToList();
            }

            dto.Nodes = flowNodes.SelectLoopChild(v => v.Super_Id, v => v.Id, (a, b) => a.Nodes = b.ToList(), null);
        }

        /// <summary>
        /// 验证流程节点是否配置正确
        /// </summary>
        /// <param name="workFlow"></param>
        private void VerifyFlowNodes(WorkFlowDto workFlow)
        {
            if (workFlow is null)
                throw new System.ArgumentNullException(nameof(workFlow));

            if (workFlow.Nodes == null || !workFlow.Nodes.Any())
                throw new MsgException("无有效节点");

            if (!workFlow.Nodes.Any(v => v.NodeEnum == FlowNodeEnum.start))
                throw new MsgException("无起点");

            if (!workFlow.Nodes.Any(v => v.NodeEnum == FlowNodeEnum.end))
                throw new MsgException("无终点");

            var nodes = workFlow.Nodes.ToArray();

            for (int i = 0; i < nodes.Length; i++)
            {
                var node = nodes[i];

                switch (node.NodeEnum)
                {
                    case FlowNodeEnum.start:
                        if (i != 0)
                            throw new MsgException("起点位置不正确!");
                        break;
                    case FlowNodeEnum.branch:
                    case FlowNodeEnum.check:
                    case FlowNodeEnum.cc:
                    case FlowNodeEnum.cond:
                        VerifyFlowNode(node);
                        break;
                    case FlowNodeEnum.end:
                        if (i != nodes.Length - 1)
                            throw new MsgException("终点位置不正确!");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 验证节点配置是否正确
        /// </summary>
        /// <param name="node"></param>
        private void VerifyFlowNode(FlowNodeModel node)
        {
            switch (node.NodeEnum)
            {
                case FlowNodeEnum.start:
                    throw new MsgException("起点位置不正确!");
                case FlowNodeEnum.branch:

                    break;
                case FlowNodeEnum.check:

                    break;
                case FlowNodeEnum.cc:

                    break;
                case FlowNodeEnum.cond:

                    break;
                case FlowNodeEnum.end:
                    throw new MsgException("终点位置不正确!");
                default:
                    break;
            }
        }

        public async Task<IEnumerable<ITreeModel>> GetChildrenBySuperId()
        {
            await Task.CompletedTask;
            var valuePairs = Loader.GetService<Infrastructure.Module.IModuleExportProvider>().HaveCheckModuleList();

            return valuePairs.Select(v => new TreeModel
            {
                ChildCount = 0,
                Id = v.Key,
                Name = v.Value,
                Super_Id = v.Key
            });
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> CheckerList(FlowNodeCheckerEnum checkerEnum, string moduleName, string kw)
        {
            switch (checkerEnum)
            {
                case FlowNodeCheckerEnum.user:
                    return await Loader
                        .GetService<IRepository<User>>()
                        .Where(v => v.Enable == Entity.Enums.EnabledMark.enabled)
                        .Where(v => kw == null || v.Name.Contains(kw) || v.Account.Contains(kw))
                        .OrderBy(v => v.Account)
                        .ThenBy(v => v.Name)
                        .Take(200)
                        .Select(v => new KeyValuePair<string, string>(v.Id, v.Account + "[" + v.Name + "]"))
                        .ToListAsync();
                case FlowNodeCheckerEnum.role:
                    return await Loader
                         .GetService<IRepository<Role>>()
                         .Where(v => !v.IsDefault)
                         .Where(v => kw == null || v.Name.Contains(kw) || v.EnCode.Contains(kw))
                         .OrderBy(v => v.EnCode)
                         .ThenBy(v => v.Name)
                         .Take(200)
                         .Select(v => new KeyValuePair<string, string>(v.Id, v.EnCode + "[" + v.Name + "]"))
                         .ToListAsync();
                case FlowNodeCheckerEnum.field:
                    if (moduleName.IsNullOrWhiteSpace())
                        return Array.Empty<KeyValuePair<string, string>>();
                    return Loader
                            .GetService<Infrastructure.Module.IModuleExportProvider>()
                            .GetModuleStruts(moduleName)
                            .FieldInfoStruts
                            .Where(v => v.Relate == nameof(User))
                            .Select(v => new KeyValuePair<string, string>(v.Name, v.Description))
                            .ToList();
                case FlowNodeCheckerEnum.dept:
                    return await Loader
                         .GetService<IRepository<Dept>>()
                         .Where(v => kw == null || v.Name.Contains(kw) || v.EnCode.Contains(kw))
                         .OrderBy(v => v.EnCode)
                         .ThenBy(v => v.Name)
                         .Take(200)
                         .Select(v => new KeyValuePair<string, string>(v.Id, v.EnCode + "[" + v.Name + "]"))
                         .ToListAsync();
                case FlowNodeCheckerEnum.prev_appoint:
                    break;
                case FlowNodeCheckerEnum.parent_dept:
                    break;
                case FlowNodeCheckerEnum.parent_dept_manage:
                    break;
                case FlowNodeCheckerEnum.dept_manage:
                    break;
                default:
                    break;
            }

            return Array.Empty<KeyValuePair<string, string>>();
        }
    }
}
