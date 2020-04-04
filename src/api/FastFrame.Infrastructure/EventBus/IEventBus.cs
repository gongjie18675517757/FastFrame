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
        Task TriggerEventAsync<T>(T @event) where T : IEventData;


        /// <summary>
        /// 触发请求
        /// </summary> 
        Task<TResult> TriggerRequestAsync<TResult, TRequest>(TRequest request);
    }
}
