﻿namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public interface ICurrUser
    {
        /// <summary>
        /// 当前ToKen的ID
        /// </summary>
        string ToKen { get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 帐号名
        /// </summary>
        string Account { get; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        bool IsAdmin { get; set; }

        /// <summary>
        /// 是否超级用户
        /// </summary>
        bool IsRoot { get; set; }  
    }

    public class CurrUser : ICurrUser
    {
        public string Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsRoot { get; set; }

        public string ToKen { get; set; }
    }
}
