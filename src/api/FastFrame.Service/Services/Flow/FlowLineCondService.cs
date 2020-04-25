using FastFrame.Dto.Flow;
using FastFrame.Entity.Flow;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastFrame.Repository;
using FastFrame.Infrastructure;
using System.Linq;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Service.Events;
using Microsoft.EntityFrameworkCore;

namespace FastFrame.Service.Services.Flow
{
    public partial class FlowLineCondService : IService,
        IEventHandle<DoMainAdding<FlowLineDto>>,
        IEventHandle<DoMainDeleteing<FlowLineDto>>,
        IEventHandle<DoMainUpdateing<FlowLineDto>>,
        IRequestHandle<IEnumerable<KeyValuePair<string, IEnumerable<FlowLineCond>>>, WorkFlowDto>
    {
        private readonly HandleOne2ManyService<FlowLineCond, FlowLineCond> manyService;
        private readonly IQueryRepository<FlowLine> flowLines;
        private readonly IQueryRepository<FlowLineCond> flowLineConds;

        public FlowLineCondService(
            HandleOne2ManyService<FlowLineCond, FlowLineCond> manyService,
            IQueryRepository<FlowLine> flowLines,
            IQueryRepository<FlowLineCond> flowLineConds)
        {
            this.manyService = manyService;
            this.flowLines = flowLines;
            this.flowLineConds = flowLineConds;
        }

        private Task HandleItems(string id, IEnumerable<FlowLineCond> items)
        {
            return manyService.UpdateManyAsync(v =>
                                        v.FlowLink_Id == id,
                                        items,
                                        (a, b) => a.Id == b.Id,
                                        v =>
                                        {
                                            v.FlowLink_Id = id;
                                            return v;
                                        },
                                        (before, after) =>
                                        {
                                            after.MapSet(before);
                                        }
                                    );
        }

        public Task HandleEventAsync(DoMainAdding<FlowLineDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Conds);
        }

        public Task HandleEventAsync(DoMainDeleteing<FlowLineDto> @event)
        {
            return HandleItems(@event.Id, null);
        }

        public Task HandleEventAsync(DoMainUpdateing<FlowLineDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Conds);
        }

        public async Task<IEnumerable<KeyValuePair<string, IEnumerable<FlowLineCond>>>> HandleRequestAsync(WorkFlowDto request)
        {
            var query = from a in flowLines
                        join b in flowLineConds on a.Id equals b.FlowLink_Id
                        where a.WorkFlow_Id == request.Id
                        select b;

            var list = await query.ToListAsync();
            return list.GroupBy(v => v.FlowLink_Id).Select(v => new KeyValuePair<string, IEnumerable<FlowLineCond>>(v.Key, v));
        }
    }
}
