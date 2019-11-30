namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 群组管理员
    /// </summary>
    public class GroupManager : IEntity
    {
        /// <summary>
        /// 群组
        /// </summary>
        public string Group_Id { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
