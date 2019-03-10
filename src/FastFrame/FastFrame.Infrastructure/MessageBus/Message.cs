namespace FastFrame.Infrastructure.MessageBus
{
    /// <summary>
    /// 消息体
    /// </summary>
    public class Message<T>
    {
        public Message(MsgType category, T content)
        {
            Category = category;
            Content = content;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public MsgType Category { get; set; }

        /// <summary>
        /// 目标用户
        /// </summary>
        public string[] Target_Ids { get; set; } = new string[0];

        /// <summary>
        /// 内容
        /// </summary>
        public T Content { get; set; }
    }
}
