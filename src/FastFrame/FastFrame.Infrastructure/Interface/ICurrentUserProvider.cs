using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 当前用户提供者
    /// </summary>
    public interface ICurrentUserProvider
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        ICurrUser GetCurrUser();

        /// <summary>
        /// 获取当前组织ID
        /// </summary>
        /// <returns></returns>
        string GetCurrOrganizeId();

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="currUser"></param>
        /// <returns></returns>
        Task Login(ICurrUser currUser);

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="currUser"></param>
        /// <returns></returns>
        Task LogOut(ICurrUser currUser);
    }
}
