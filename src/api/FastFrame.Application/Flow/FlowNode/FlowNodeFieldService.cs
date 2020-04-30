using FastFrame.Application.Events;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Flow
{
    public partial class FlowNodeFieldService : IService,
        IEventHandle<DoMainAdding<FlowNodeDto>>,
        IEventHandle<DoMainUpdateing<FlowNodeDto>>,
        IEventHandle<DoMainDeleteing<FlowNodeDto>>,
        IRequestHandle<IEnumerable<KeyValuePair<string, IEnumerable<FlowNodeField>>>, WorkFlowDto>
    {
        private readonly HandleOne2ManyService<string, FlowNodeField> manyService;
        private readonly IQueryRepository<FlowNode> flowNodes;
        private readonly IQueryRepository<FlowNodeField> flowNodeFields;

        public FlowNodeFieldService(HandleOne2ManyService<string, FlowNodeField> manyService,
                                    IQueryRepository<FlowNode> flowNodes,
                                    IQueryRepository<FlowNodeField> flowNodeFields)
        {
            this.manyService = manyService;
            this.flowNodes = flowNodes;
            this.flowNodeFields = flowNodeFields;
        }

        public Task HandleItems(string id, IEnumerable<string> items)
        {
            return manyService.UpdateManyAsync(v => v.FlowNode_Id == id, items, (a, b) => a.FieldName == b, v => new FlowNodeField { FlowNode_Id = id, FieldName = v });
        }

        public Task HandleEventAsync(DoMainAdding<FlowNodeDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Fields);
        }

        public Task HandleEventAsync(DoMainUpdateing<FlowNodeDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Fields);
        }

        public Task HandleEventAsync(DoMainDeleteing<FlowNodeDto> @event)
        {
            return HandleItems(@event.Id, null);
        }

        public async Task<IEnumerable<KeyValuePair<string, IEnumerable<FlowNodeField>>>> HandleRequestAsync(WorkFlowDto request)
        {
            var query = from a in flowNodes
                        join b in flowNodeFields on a.Id equals b.FlowNode_Id
                        where a.WorkFlow_Id == request.Id
                        select b;
            var list = await query.ToListAsync();

            return list.GroupBy(v => v.FlowNode_Id).Select(v => new KeyValuePair<string, IEnumerable<FlowNodeField>>(v.Key, v));
        }
    }
}
