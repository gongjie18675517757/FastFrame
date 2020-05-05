using FastFrame.Entity;
using System.Threading.Tasks;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository.Events;

namespace FastFrame.Application
{
    ///// <summary>
    ///// 处理自动编号
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class HandleHaveNumberService<T> : IEventHandle<EntityAdding<T>> where T : IHaveNumber
    //{
    //    private readonly IAutoNumberService numberService;

    //    public HandleHaveNumberService(IAutoNumberService numberService)
    //    {
    //        this.numberService = numberService;
    //    }
    //    public async Task HandleEventAsync(EntityAdding<T> @event)
    //    {
    //        await numberService.MakeNumberAsync(@event.Data); 
    //    }
    //}
}
