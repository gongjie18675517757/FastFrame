using FastFrame.Dto.Flow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FastFrame.Service.Services.Flow
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
