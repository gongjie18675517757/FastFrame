namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 事件通知
    /// </summary>
    public enum FlowNodeEventNotifyEnum
    {
        /// <summary>
        /// 微信通知
        /// </summary>
        wx_notify = 0,

        /// <summary>
        /// 应用内通知
        /// </summary>
        app_notify = 10,

        /// <summary>
        /// 短信通知
        /// </summary>
        sms_notify = 20,

        /// <summary>
        /// 邮箱通知
        /// </summary>
        email_notify = 30,
    }
}
