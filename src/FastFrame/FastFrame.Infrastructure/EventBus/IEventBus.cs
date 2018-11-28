using FastFrame.Infrastructure.Interface;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.EventBus
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 触发事件
        /// </summary> 
        Task TriggerAsync<T>(IEventData<T> @event);
    }
}
