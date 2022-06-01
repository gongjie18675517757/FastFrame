namespace FastFrame.Infrastructure.Identity
{
    /// <summary>
    /// 身份配置
    /// </summary>
    public class IdentityConfig
    {
        /// <summary>
        /// 允许连续失败次数
        /// </summary>
        public int FailCount { get; set; } = 5;

        /// <summary>
        /// 允许连续失败的时间
        /// </summary>
        public TimeSpan FailTime { get; set; } = TimeSpan.FromMinutes(10);

        /// <summary>
        /// Token有效时间
        /// </summary>
        public TimeSpan TokenEffectiveTime { get; set; } = TimeSpan.FromDays(1);
    }
}
