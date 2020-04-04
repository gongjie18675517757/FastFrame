using System.Threading.Tasks;

namespace FastFrame.Infrastructure.EventBus
{
    /// <summary>
    /// 事件处理
    /// </summary> 
    public interface IRequestHandle<TResult, TRequest>
    {
        Task<TResult> HandleRequestAsync(TRequest request);
    }
}
