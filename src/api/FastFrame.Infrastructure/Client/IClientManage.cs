using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Client
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
}
