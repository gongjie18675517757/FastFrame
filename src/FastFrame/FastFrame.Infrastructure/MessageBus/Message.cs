namespace FastFrame.Infrastructure.MessageBus
{
    /// <summary>
    /// 消息体
    /// </summary>
    public class Message<T> where T : class
    {
        /// <summary>
        /// 类型
        /// </summary>
        public MsgType Category { get; set; }

        /// <summary>
        /// 目标用户
        /// </summary>
        public string[] Target_Ids { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public T Content { get; set; }
    }
}
