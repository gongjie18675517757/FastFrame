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
        /// <returns></returns>
        Task PublishAsync(string qname, string msg); 
    }
}
