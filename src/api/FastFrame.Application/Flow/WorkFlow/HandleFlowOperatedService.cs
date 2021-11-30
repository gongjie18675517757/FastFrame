using FastFrame.Entity;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 处理流程审核完成后的事件
    /// </summary>
    /// <typeparam name="TBillEntity"></typeparam>
    public class HandleFlowOperatedService<TBillEntity> : IEventHandle<FlowOperated<TBillEntity>> where TBillEntity : IHaveCheck
    {

        public async Task HandleEventAsync(FlowOperated<TBillEntity> @event)
        {
            await Task.CompletedTask;

            /*在这里推送事件等*/


        }
    }
}
