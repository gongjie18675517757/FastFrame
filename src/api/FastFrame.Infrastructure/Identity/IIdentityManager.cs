using System.Net;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Identity
{
    /// <summary>
    /// 身份管理
    /// </summary>
    public interface IIdentityManager
    {
        /// <summary>
        /// 验证TOKEN是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<bool> ExistsTokenAsync(string token, IPAddress address);

        /// <summary>
        /// 生成身份
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<IIdentity> GenerateIdentity(string userId, IPAddress address);

        /// <summary>
        /// 刷新TOKEN
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RefreshTokenAsync(string token);

        /// <summary>
        /// 强制TOKEN失效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SetTokenFailureAsync(string token);

        /// <summary>
        /// 强制用户所有TOKEN失效
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task SetUserAllTokenFailureAsync(string userId);
    }
}
