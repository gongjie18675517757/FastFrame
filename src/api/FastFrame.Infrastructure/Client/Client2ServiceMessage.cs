namespace FastFrame.Infrastructure.Client
{
    /// <summary>
    /// 服务端消息
    /// </summary>
    public class Client2ServiceMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public string MsgContent { get; set; }
    }
}
