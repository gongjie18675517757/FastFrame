using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 当前用户提供者
    /// </summary>
    public interface IAppSessionProvider
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
    }
}
