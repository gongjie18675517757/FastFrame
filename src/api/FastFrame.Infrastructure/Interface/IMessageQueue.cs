using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 消息队列
    /// </summary>
    public interface IMessageQueue
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="qname">消息名</param>
        /// <param name="msg">消息体</param>
        /// <returns>消息ID</returns>
        Task<string> PublishAsync(string qname, string msg);

        /// <summary>
        /// 通知客户端
        /// </summary>
        const string ClientMessage = "client.message"; 
    }
}
