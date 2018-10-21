﻿namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public interface ICurrUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 组织ID
        /// </summary>
        string OrganizeId { get; }
    }
}
