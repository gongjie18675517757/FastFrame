using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Client
{
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
}
