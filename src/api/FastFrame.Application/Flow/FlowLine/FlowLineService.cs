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
    public partial class FlowLineService : IService,
        IEventHandle<DoMainAdding<WorkFlowDto>>,
        IEventHandle<DoMainDeleteing<WorkFlowDto>>,
        IEventHandle<DoMainUpdateing<WorkFlowDto>>,
        IRequestHandle<IEnumerable<FlowLineDto>, WorkFlowDto>
    {
        private readonly HandleOne2ManyService<FlowLineDto, FlowLine> manyService;
        private readonly IQueryRepository<FlowLine> flowLines;
        private readonly IEventBus eventBus;

        public FlowLineService(HandleOne2ManyService<FlowLineDto, FlowLine> manyService, IQueryRepository<FlowLine> flowLines, IEventBus eventBus)
        {
            this.manyService = manyService;
            this.flowLines = flowLines;
            this.eventBus = eventBus;
        }

        private async Task HandleItemsAsync(string id, IEnumerable<FlowLineDto> items)
        {
            await manyService
                    .UpdateManyAsync(
                            v => v.WorkFlow_Id == id,
                            items,
                            (a, b) => a.Id == b.Id,
                            v =>
                            {
                                var itemEntity = v.MapTo<FlowLineDto, FlowLine>();
                                itemEntity.WorkFlow_Id = id;
                                return itemEntity;
                            },
                            (before, after) =>
                            {
                                after.MapSet(before);
                            });
        } 


        public Task HandleEventAsync(DoMainAdding<WorkFlowDto> @event)
        {
            return HandleItemsAsync(@event.Data.Id, @event.Data.Lines);
        }

        public Task HandleEventAsync(DoMainDeleteing<WorkFlowDto> @event)
        {
            return HandleItemsAsync(@event.Id, null);
        }

        public Task HandleEventAsync(DoMainUpdateing<WorkFlowDto> @event)
        {
            return HandleItemsAsync(@event.Data.Id, @event.Data.Lines);
        }

        public async Task<IEnumerable<FlowLineDto>> HandleRequestAsync(WorkFlowDto request)
        {
            var list = await flowLines.Where(v => v.WorkFlow_Id == request.Id).MapTo<FlowLine, FlowLineDto>().ToListAsync();
            var keys = list.Select(v => v.Id).ToArray();
            var condKvs = await eventBus.RequestAsync<IEnumerable<KeyValuePair<string, IEnumerable<FlowLineCond>>>, string[]>(keys);
            foreach (var item in list)
            {
                item.Conds = condKvs.Where(v => v.Key == item.Id).SelectMany(v => v.Value);
            }
            return list;
        }
    }
}
