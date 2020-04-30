using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Application.Flow
{
    public partial class WorkFlowService
    {
        protected override async Task OnGeting(WorkFlowDto dto)
        {
            await base.OnGeting(dto);
            dto.Lines = await EventBus.TriggerRequestAsync<IEnumerable<FlowLineDto>, WorkFlowDto>(dto);
            dto.Nodes = await EventBus.TriggerRequestAsync<IEnumerable<FlowNodeDto>, WorkFlowDto>(dto);
        }
    }
}
