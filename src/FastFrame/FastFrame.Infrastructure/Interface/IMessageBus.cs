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
        Task PubLishAsync<T>(Message<T> message);
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 通知
        /// </summary>
        Notify,

        /// <summary>
        /// 好友消息
        /// </summary>       
        FriendMsg,

        /// <summary>
        /// 群组消息
        /// </summary>
        GroupMsg,
    }

    /// <summary>
    /// 消息体
    /// </summary>
    public class Message<T>
    { 
        /// <summary>
        /// 类型
        /// </summary>
        public MsgType Category { get; set; }

        /// <summary>
        /// 目标用户
        /// </summary>
        public string[] Target_Ids { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public T Content { get; set; }
    }
}
