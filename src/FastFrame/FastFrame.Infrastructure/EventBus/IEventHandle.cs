using System.Threading.Tasks;

namespace FastFrame.Infrastructure.EventBus
{
    /// <summary>
    /// 事件处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHandle<T>
    {

        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleEventAsync(IEventData<T> @event);
    }
}
