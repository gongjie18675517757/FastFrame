namespace FastFrame.Infrastructure.Client
{
    /// <summary>
    /// 客户端确认结果
    /// </summary>
    public class ClientConfirmResult
    {
        /// <summary>
        /// 确认会话ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 确认结果
        /// </summary>
        public bool Result { get; set; }
    }
}
