namespace FastFrame.Infrastructure.Client
{
    /// <summary>
    /// 客户端选择结果
    /// </summary>
    public class ClientChooseResult
    {
        /// <summary>
        /// 确认会话ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 选择结果
        /// </summary>
        public string[] Result { get; set; }
    }
}
