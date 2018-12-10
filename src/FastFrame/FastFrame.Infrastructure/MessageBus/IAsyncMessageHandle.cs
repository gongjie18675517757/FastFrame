using System.Threading.Tasks;

namespace FastFrame.Infrastructure.MessageBus
{
    /// <summary>
    /// 处理异步消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncMessageHandle<T> : IAsyncMessageHandle where T : class,new()
    {
        Task Handle(Message<T> message);
    }
}
