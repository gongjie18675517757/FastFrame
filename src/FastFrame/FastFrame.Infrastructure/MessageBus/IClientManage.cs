using System.Threading.Tasks;

namespace FastFrame.Infrastructure.MessageBus
{
    /// <summary>
    /// 客户端连接管理
    /// </summary>
    public interface IClientManage
    {
        Task SendAsync<T>(Message<T> message);
    }
}
