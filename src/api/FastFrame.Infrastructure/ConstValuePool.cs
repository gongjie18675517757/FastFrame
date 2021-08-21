namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 常量池
    /// </summary>
    public static class ConstValuePool
    {
        /// <summary>
        /// 缓存用户连接信息
        /// </summary>
        public const string CacheUserMapKey = "CacheUserMapKey";

        /// <summary>
        /// 存用户身份的cookie键
        /// </summary>

        public const string Token_Name = "Authorize";

        /// <summary>
        /// 缓存租户信息键
        /// </summary>
        public const string CacheTenant = "Cache:Multi-Tenant-List";

        /// <summary>
        /// 缓存租户HOST信息键
        /// </summary>
        public const string CacheTenantHost = "Cache:Multi-TenantHost-List";
    }
}
