using FastFrame.Infrastructure.Identity;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 当前会话信息
    /// </summary>
    public interface IApplicationSession
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        ICurrUser CurrUser { get; }

        /// <summary>
        /// 当前租户(为Null则表示为根租户)
        /// </summary>
        /// <returns></returns>
        string Tenant_Id { get; }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="currUser"></param>
        /// <returns></returns>
        Task LoginAsync(ICurrUser currUser);

        /// <summary>
        /// 登出
        /// </summary> 
        Task LogOutAsync();

        /// <summary>
        /// 刷新身份
        /// </summary>
        /// <returns></returns>
        Task RefreshIdentityAsync();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        Task InitAsync();

        /// <summary>
        /// 获取当前连接的IP
        /// </summary>
        /// <returns></returns>
        IPAddress GetIPAddress();

        /// <summary>
        /// 运行目录
        /// </summary>
        string ApplicationRootPath { get; }
    } 
}
