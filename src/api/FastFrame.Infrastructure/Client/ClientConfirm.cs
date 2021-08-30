namespace FastFrame.Infrastructure.Client
{
    /// <summary>
    /// 客户端确认
    /// </summary>
    public class ClientConfirm
    {
        public ClientConfirm()
        {
            Id = IdGenerate.NetId();
        }

        /// <summary>
        /// 确认会话ID
        /// </summary>
        public virtual string Id { get; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 超时(秒)
        /// </summary>
        public int Timeout { get; set; } = 10;
    }
}
