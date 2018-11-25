using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 消息总线
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        ///  发布消息
        /// </summary>      
        Task PubLishAsync(Message message);
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 通知
        /// </summary>
        Notify = 0,

    }

    /// <summary>
    /// 消息体
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string[] FromUserIds { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string[] ToUserIds { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MessageType Category { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

    }
}
