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
using FastFrame.Infrastructure.Module;
using FastFrame.Entity;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 流程管理
    /// </summary>
    public partial class WorkFlowService :
        IRequestHandle<FlowNodeModel[], string>,
        IRequestHandle<FlowNodeModel[], string[]>
    {
        public async Task<int> GetLastVersion(string moduleName)
        {
            return await workFlowRepository.Where(v => v.BeModule == moduleName).Select(v => v.Version).OrderByDescending(v => v).FirstOrDefaultAsync() + 1;
        }

        public async Task<FlowNodeModel[]> HandleRequestAsync(string request)
        {
            if (request.IsNullOrWhiteSpace())
                return null;

            var id = request;
            var ids = await Loader.GetService<IRepository<FlowNode>>().Where(v => v.WorkFlow_Id == id).Select(v => v.Id).ToArrayAsync();
            var flowNodes = await HandleRequestAsync(ids);

            return flowNodes
                .OrderBy(v => v.OrderVal)
                .ThenBy(v => v.Id)
                .SelectLoopChild(v => v.Super_Id, v => v.Id, (a, b) => a.Nodes = b.ToList(), null)
                .ToArray();
        }

        public async Task<FlowNodeModel[]> HandleRequestAsync(string[] request)
        {
            var flowNodes = await Loader.GetService<IRepository<FlowNode>>().Where(v => request.Contains(v.Id)).MapTo<FlowNode, FlowNodeModel>().ToArrayAsync();
            var flowNodeConds = await Loader.GetService<IRepository<FlowNodeCond>>().Where(v => request.Contains(v.FlowNode_Id)).ToListAsync();
            var flowNodeEvents = await Loader.GetService<IRepository<FlowNodeEvent>>().Where(v => request.Contains(v.FlowNode_Id)).ToListAsync();
            var flowNodeCheckers = await Loader.GetService<IRepository<FlowNodeChecker>>().Where(v => request.Contains(v.FlowNode_Id)).ToListAsync();

            foreach (var node in flowNodes)
            {
                node.Conds = flowNodeConds
                    .Where(v => v.FlowNode_Id == node.Id)
                    .GroupBy(v => v.GroupIndex)
                    .OrderBy(v => v.Key)
                    .Select(v => v.ToArray())
                    .ToList();
                node.Events = flowNodeEvents.Where(v => v.FlowNode_Id == node.Id).ToList();
                node.Checkers = flowNodeCheckers.Where(v => v.FlowNode_Id == node.Id).ToList();
            }

            return request.SelectMany(v => flowNodes.Where(f => f.Id == v)).ToArray();
        }

        protected override async Task OnAdding(WorkFlowDto input, WorkFlow entity)
        {
            entity.Version = await GetLastVersion(entity.BeModule);
            await base.OnAdding(input, entity);
        }

        protected override async Task OnBeforeUpdate(WorkFlow before, WorkFlowDto after)
        {
            /*判断流程有没有被使用，有则不允许修改*/
            if (await Loader.GetService<IRepository<FlowInstance>>().AnyAsync(v => v.WorkFlow_Id == before.Id /*&& !v.IsComlete*/))
            {
                /*判断节点是否改变了*/
                var beforeList = await Loader
                    .GetService<IRepository<FlowNode>>()
                    .Where(v => v.WorkFlow_Id == before.Id)
                    .OrderBy(v => v.OrderVal)
                    .Select(v => v.Id)
                    .ToArrayAsync();

                var afterNodeList = after?.Nodes.SelectLoopChild(v => v.Nodes).ToArray();

                if (beforeList.Length != afterNodeList.Length)
                    throw new MsgException("此流程已被使用过，不允许再修改,可以复制当前流程并生成一个新的版本！");

                for (int i = 0; i < afterNodeList.Length; i++)
                {
                    if (beforeList[i] != afterNodeList[i].Id)
                        throw new MsgException("此流程已被使用过，不允许再修改,可以复制当前流程并生成一个新的版本！");
                }
            }

            await base.OnBeforeUpdate(before, after);
        }

        protected override async Task OnAddOrUpdateing(WorkFlowDto input, WorkFlow entity)
        {
            /*验证流程节点是否配置正确*/
            VerifyFlowNodes(input);

            if (TypeManger.TryGetType(entity.BeModule, out var type))
                entity.BeModuleName = Loader.GetService<IModuleDesProvider>().GetClassDescription(type);

            await base.OnAddOrUpdateing(input, entity);
        }

        protected override async Task OnChangeing(WorkFlowDto input, WorkFlow entity)
        {
            await base.OnChangeing(input, entity);

            var id = entity.Id;
            var flowNodes = Loader.GetService<IRepository<FlowNode>>();
            var afterNodeList = input?.Nodes.SelectLoopChild(v => v.Nodes).ToArray();

            /*更新格式*/
            input?.Nodes.EachLoopChild(v => v.Nodes, null, temp =>
            {
                var (patent, child) = temp;
                if (child.NodeEnum == FlowNodeEnum.branch)
                {
                    foreach (var node in child.Nodes)
                    {
                        node.IsDefault = false;
                        node.NodeEnum = FlowNodeEnum.branch_child;
                    }

                    child.Nodes.LastOrDefault().IsDefault = true;
                }

                if (patent?.NodeEnum == FlowNodeEnum.branch_child && child?.NodeEnum == FlowNodeEnum.cond)
                {
                    child.IsDefault = patent.IsDefault;

                    patent.Weight = child.Weight;
                }
            });

            for (int i = 0; i < afterNodeList.Length; i++)
                afterNodeList[i].OrderVal = i + 1;

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

            /*更新ID*/
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
                    afterNodeList?
                        .Where(v => v.Conds != null)
                        .SelectMany(v =>
                            v.Conds.SelectMany((x, xIndex) => x.Select(c =>
                              {
                                  c.FlowNode_Id = v.Id;
                                  c.WorkFlow_Id = id;
                                  c.GroupIndex = xIndex;
                                  return c;
                              }))),
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
                    afterNodeList?.Where(v => v.Events != null).SelectMany(v => v.Events.Select(x =>
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
                    afterNodeList?.Where(v => v.Checkers != null).SelectMany(v => v.Checkers.Select(x =>
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
            dto.Nodes = await HandleRequestAsync(dto.Id);
        }

        /// <summary>
        /// 验证流程节点是否配置正确
        /// </summary>
        /// <param name="workFlow"></param>
        private void VerifyFlowNodes(WorkFlowDto workFlow)
        {
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
                            .Where(v => kw.IsNullOrWhiteSpace() || v.Description.Contains(kw))
                            .Select(v => new KeyValuePair<string, string>(v.Name, v.Description))
                            .ToList();
                case FlowNodeCheckerEnum.dept_manage:
                case FlowNodeCheckerEnum.dept:
                    return await Loader
                         .GetService<IRepository<Dept>>()
                         .Where(v => kw == null || v.Name.Contains(kw) || v.TreeCode.Contains(kw))
                         .OrderBy(v => v.TreeCode)
                         .ThenBy(v => v.Name)
                         .Take(200)
                         .Select(v => new KeyValuePair<string, string>(v.Id, v.TreeCode + "[" + v.Name + "]"))
                         .ToListAsync();
                case FlowNodeCheckerEnum.prev_appoint:
                    break;
                case FlowNodeCheckerEnum.parent_dept:
                    break;
                case FlowNodeCheckerEnum.parent_dept_manage:
                    break;
                case FlowNodeCheckerEnum.cur_dept_manage:
                    break;
                default:
                    break;
            }

            return Array.Empty<KeyValuePair<string, string>>();
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> RelateKvs(string entityName, string kw)
        {
            if (TypeManger.TryGetType(entityName, out var type) && typeof(IEntity).IsAssignableFrom(type))
            {
                var repositoryType = typeof(IRepository<>).MakeGenericType(type);
                var queryable = (IQueryable)Loader.GetService(repositoryType);
                var fields = Array.Empty<string>();
                if (type.GetCustomAttribute<RelatedFieldAttribute>() is RelatedFieldAttribute relatedField)
                    fields = relatedField.FieldNames.ToArray();
                else
                    fields = type
                        .GetProperties()
                        .Where(v => !v.Name.EndsWith("_Id") && v.PropertyType == typeof(string))
                        .Select(v => v.Name)
                        .ToArray();


                if (!kw.IsNullOrWhiteSpace())
                    queryable = queryable
                        .Where(string.Join(" and ", fields.Select(v => $"{v}.Contains(@0)")), kw);

                var dynamics = await queryable
                    .Select($"new {{ Id,{string.Join(",", fields)}}}")
                    .OrderBy(string.Join(",", fields))
                    .Take(200)
                    .ToDynamicListAsync();

                var list = dynamics.ToJson().ToObject<Dictionary<string, string>[]>();

                return list
                    .Select(v =>
                        new KeyValuePair<string, string>(
                            v.TryGetValueOrDefault("Id"),
                            string
                                .Join(
                                    "",
                                    fields
                                        .Select((f, fIndex) =>
                                        {
                                            var val = v.TryGetValueOrDefault(f);
                                            if (val.IsNullOrWhiteSpace())
                                                return null;

                                            if (fIndex == 0)
                                                return val;
                                            else
                                                return $"[{val}]";
                                        })
                                        .Where(v => !v.IsNullOrWhiteSpace()))));
            }
            else
            {
                return Array.Empty<KeyValuePair<string, string>>();
            }
        }


        public async Task ToggleEnable(string id)
        {
            var entity = await workFlowRepository.GetAsync(id);
            if (entity == null)
                throw new NotFoundException();

            switch (entity.Enabled)
            {
                case Entity.Enums.EnabledMark.enabled:
                    entity.Enabled = Entity.Enums.EnabledMark.disabled;
                    break;
                case Entity.Enums.EnabledMark.disabled:
                    entity.Enabled = Entity.Enums.EnabledMark.enabled;
                    break;
                default:
                    break;
            }

            await workFlowRepository.UpdateAsync(entity);
            await workFlowRepository.CommmitAsync();
        }
    }


}
