namespace FastFrame.Infrastructure.Client
{
    /// <summary>
    /// 客户端通知
    /// </summary>
    public class ClientNotify
    {
        /// <summary>
        /// 通知ID
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 通知标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string ModuleName { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public virtual string ToUrl { get; set; }
    }
}
