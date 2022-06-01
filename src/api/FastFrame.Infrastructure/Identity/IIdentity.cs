using System;

namespace FastFrame.Infrastructure.Identity
{
    /// <summary>
    /// 身份接口
    /// </summary>
    public interface IIdentity
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        bool IsEnabled { get; set; } 

        /// <summary>
        /// 用户ID
        /// </summary>
        string User_Id { get; set; }

        /// <summary>
        /// 获取TOKEN
        /// </summary>
        /// <returns></returns>
        string GetToken();
    }
}
