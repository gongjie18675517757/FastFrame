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

        /// <summary>
        /// 数据新增
        /// </summary>
        DataAdded,

        /// <summary>
        /// 数据更新
        /// </summary>
        DataUpdated,

        /// <summary>
        /// 数据删除
        /// </summary>
        DataDeleted
    }
}
