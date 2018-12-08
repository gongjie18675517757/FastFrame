namespace FastFrame.Infrastructure.MessageBus
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 通知
        /// </summary>
        Notify,

        /// <summary>
        /// 好友消息
        /// </summary>       
        FriendMsg,

        /// <summary>
        /// 群组消息
        /// </summary>
        GroupMsg,
    }
}
