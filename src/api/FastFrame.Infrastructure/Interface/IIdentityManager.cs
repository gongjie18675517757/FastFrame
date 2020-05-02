﻿using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    public interface IIdentityManager
    {
        /// <summary>
        /// 验证TOKEN是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> ExistsTokenAsync(string token);

        /// <summary>
        /// 生成身份
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IIdentity> GenerateIdentity(string userId); 

        /// <summary>
        /// 刷新TOKEN
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RefreshTokenAsync(string token);
    }
}
