﻿using FastFrame.Application.Basis;
using FastFrame.Application.Events;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Flow
{
    public partial class FlowNodeService : IService,
        IEventHandle<DoMainAdding<WorkFlowDto>>,
        IEventHandle<DoMainDeleteing<WorkFlowDto>>,
        IEventHandle<DoMainUpdateing<WorkFlowDto>>,
        IRequestHandle<IEnumerable<FlowNodeDto>, WorkFlowDto>
    {
        private readonly HandleOne2ManyService<FlowNodeDto, FlowNode> manyService;
        private readonly IQueryRepository<FlowNode> flowNodes;
        private readonly IEventBus eventBus;

        public FlowNodeService(HandleOne2ManyService<FlowNodeDto, FlowNode> manyService, IQueryRepository<FlowNode> flowNodes, IEventBus eventBus)
        {
            this.manyService = manyService;
            this.flowNodes = flowNodes;
            this.eventBus = eventBus;
        }

        public Task HandleEventAsync(DoMainAdding<WorkFlowDto> @event)
        {
            return HandleItemsAsync(@event.Data.Id, @event.Data.Nodes);
        }

        public Task HandleEventAsync(DoMainDeleteing<WorkFlowDto> @event)
        {
            return HandleItemsAsync(@event.Id, null);
        }

        public Task HandleEventAsync(DoMainUpdateing<WorkFlowDto> @event)
        {
            return HandleItemsAsync(@event.Data.Id, @event.Data.Nodes);
        }

        public async Task<IEnumerable<FlowNodeDto>> HandleRequestAsync(WorkFlowDto request)
        {
            var list = await flowNodes.Where(v => v.WorkFlow_Id == request.Id).MapTo<FlowNode, FlowNodeDto>().ToListAsync();

            var fieldKvs = await eventBus.TriggerRequestAsync<IEnumerable<KeyValuePair<string, IEnumerable<FlowNodeField>>>, WorkFlowDto>(request);
            var userKvs = await eventBus.TriggerRequestAsync<IEnumerable<KeyValuePair<string, IEnumerable<UserViewModel>>>, WorkFlowDto>(request);
            var roleKvs = await eventBus.TriggerRequestAsync<IEnumerable<KeyValuePair<string, IEnumerable<RoleViewModel>>>, WorkFlowDto>(request);
            foreach (var item in list)
            {
                item.Fields = fieldKvs.Where(v => v.Key == item.Id).SelectMany(v => v.Value.Select(r => r.FieldName));
                item.Roles = roleKvs.Where(v => v.Key == item.Id).SelectMany(v => v.Value);
                item.Users = userKvs.Where(v => v.Key == item.Id).SelectMany(v => v.Value);
            }
            return list;
        }

        private async Task HandleItemsAsync(string id, IEnumerable<FlowNodeDto> items)
        {
            await manyService.UpdateManyAsync(
                                    v => v.WorkFlow_Id == id,
                                    items,
                                    (a, b) => a.Id == b.Id,
                                    v =>
                                    {
                                        var itemEntity = v.MapTo<FlowNodeDto, FlowNode>();
                                        itemEntity.WorkFlow_Id = id;
                                        return itemEntity;
                                    },
                                    (before, after) =>
                                    {
                                        after.MapSet(before);
                                    }
                            );
        }
    }
}
