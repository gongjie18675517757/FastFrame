using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 客户端管理
    /// </summary>
    public interface IClientManage
    {
        /// <summary>
        /// 给指定用户发送消息(发布到消息队列)
        /// </summary> 
        Task<string> PublishSendAsync(string msgType, string msgContent, string[] userIds);

        /// <summary>
        /// 通知用户(发布到消息队列)
        /// </summary> 
        Task PublishNotifyAsync(ClientNotify notify, string[] userIds);

        /// <summary>
        /// 提示用户确认(发布到消息队列)
        /// </summary> 
        Task<bool> PublishConfirmAsync(ClientConfirm clientConfirm, string userId);

        /// <summary>
        /// 提示用户选择(发布到消息队列)
        /// </summary> 
        Task<string[]> PublishChooseAsync(ClientChoose clientChoose, string userId);

        /// <summary>
        /// 检查是否在线
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>在线的连接名</returns>
        IAsyncEnumerable<string> ExistsIsOnLine(string userId);

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
    /// 抽象客户端连接
    /// </summary>
    public interface IClientConnection
    {
        /// <summary>
        /// 发送消息(直接发送)
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="msg_type"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        Task SendAsync(string[] userIds, string msg_type, params object[] arguments);

        /// <summary>
        /// 检查是否在线
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> ExistsIsOnLine(string userId);

        /// <summary>
        /// 连接名称
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// 服务端消息
    /// </summary>
    public class Client2ServiceMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public string MsgContent { get; set; }
    }

    /// <summary>
    /// 客户端消息
    /// </summary>
    public class Service2ClientMessage : Client2ServiceMessage
    {
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

    /// <summary>
    /// 客户端确认
    /// </summary>
    public class ClientConfirm
    {
        public ClientConfirm()
        {
            Id = IdGenerate.NetId();
        }

        /// <summary>
        /// 确认会话ID
        /// </summary>
        public virtual string Id { get; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 超时(秒)
        /// </summary>
        public int Timeout { get; set; } = 10;
    }

    /// <summary>
    /// 客户端确认结果
    /// </summary>
    public class ClientConfirmResult
    {
        /// <summary>
        /// 确认会话ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 确认结果
        /// </summary>
        public bool Result { get; set; }
    }

    /// <summary>
    /// 客户端选择
    /// </summary>
    public class ClientChoose
    {
        public ClientChoose()
        {
            Id = IdGenerate.NetId();
        }

        /// <summary>
        /// 确认会话ID
        /// </summary>
        public virtual string Id { get; }

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 提示文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 可选值列表
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Values { get; set; }

        /// <summary>
        /// 超时(秒)
        /// </summary>
        public int Timeout { get; set; } = 10;
    }

    /// <summary>
    /// 客户端选择结果
    /// </summary>
    public class ClientChooseResult
    {
        /// <summary>
        /// 确认会话ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 选择结果
        /// </summary>
        public string[] Result { get; set; }
    }
}
