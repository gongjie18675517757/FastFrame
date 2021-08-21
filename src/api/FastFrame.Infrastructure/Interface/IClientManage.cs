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
        /// <param name="msgType"></param>
        /// <param name="msgContent"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task SendAsync(string msgType, string msgContent, params string[] userIds);

        /// <summary>
        /// 给全部用户发送消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msgContent"></param> 
        /// <returns></returns>
        Task SendAllAsync(string msgType, string msgContent);


        /// <summary>
        /// 提示用户确认
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<bool> ConfirmAsync(string userId, string title, string content, int timeout);

        /// <summary>
        /// 提示用户选择
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="values"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<bool> ChooseAsync(string userId, string title, string text, KeyValuePair<string, string> values, int timeout);
    }
}
