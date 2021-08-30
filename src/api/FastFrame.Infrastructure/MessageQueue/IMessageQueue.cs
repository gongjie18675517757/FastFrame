using System.Threading.Tasks;

namespace FastFrame.Infrastructure.MessageQueue
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
        /// 服务端发送给客户端
        /// </summary>
        const string Service2ClientMessage = "service.message.to.client";

        /// <summary>
        /// 客户端发送给服务端
        /// </summary>
        const string Client2ServiceMessage = "client.message.to.service";
    }
}
