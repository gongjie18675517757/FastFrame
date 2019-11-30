namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 邮件正文
    /// </summary>
    public class EmailContent : IEntity
    {
        /// <summary>
        /// 邮件ID
        /// </summary>
        public string Email_Id { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
