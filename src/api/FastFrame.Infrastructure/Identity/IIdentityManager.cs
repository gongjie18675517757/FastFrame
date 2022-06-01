using System.Net;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Identity
{
    /// <summary>
    /// 身份管理
    /// </summary>
    public interface IIdentityManager
    {
        public delegate bool VerifyIdentity(string password, out Exception exception);

        /// <summary>
        /// 验证TOKEN是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<bool> ExistsTokenAsync(string token, IPAddress address); 

        /// <summary>
        /// 刷新TOKEN
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        void RefreshToken(string token);

        /// <summary>
        /// 强制TOKEN失效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        void SetTokenFailure(string token);

        /// <summary>
        /// 强制用户所有TOKEN失效
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        void SetUserAllTokenFailure(string userId);

        /// <summary>
        /// 尝试生成身份
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="address"></param>
        /// <param name="password"></param>
        /// <param name="verifyIdentity"></param> 
        /// <returns></returns>
        Task<IIdentity> TryGenerateIdentity(string userId,
                                 IPAddress address,
                                 string password,
                                 VerifyIdentity verifyIdentity); 
    }
}
