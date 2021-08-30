namespace FastFrame.Infrastructure.Client
{
    /// <summary>
    /// 客户端消息
    /// </summary>
    public class Service2ClientMessage : Client2ServiceMessage
    {
        /// <summary>
        /// 要通知的人
        /// </summary>
        public string[] ToUser { get; set; }
    }
}
