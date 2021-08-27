using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 客户端连接管理
    /// </summary>
    public interface IClientManage
    {
        /// <summary>
        /// 给指定用户发送消息
        /// </summary> 
        Task<string> PublishSendAsync(string msgType, string msgContent, string[] userIds); 

        /// <summary>
        /// 通知用户
        /// </summary> 
        Task PublishNotifyAsync(ClientNotify notify, string[] userIds);  

        /// <summary>
        /// 提示用户确认
        /// </summary> 
        Task<bool> PublishConfirmAsync(string userId, string title, string content, int timeout);

        /// <summary>
        /// 提示用户选择
        /// </summary> 
        Task<bool> PublishChooseAsync(string userId, string title, string text, KeyValuePair<string, string> values, int timeout);

        /// <summary>
        /// 通知客户端
        /// </summary>
        const string ClientNotify = "client.notify";

        /// <summary>
        /// 要求客户端确认
        /// </summary>
        const string ClientConfirm = "client.confirm";

        /// <summary>
        /// 要求客户端选择
        /// </summary>
        const string ClientChoose = "client.choose";
    }

    /// <summary>
    /// 客户端消息
    /// </summary>
    public class ClientMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public string MsgContent { get; set; }

        /// <summary>
        /// 要通知的人
        /// </summary>
        public string[] ToUser { get; set; }
    }

    /// <summary>
    /// 客户端通知
    /// </summary>
    public class ClientNotify
    {
        /// <summary>
        /// 通知ID
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 通知标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string ModuleName { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public virtual string ToUrl { get; set; }
    }
}
