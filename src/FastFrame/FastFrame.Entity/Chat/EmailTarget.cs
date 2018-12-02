namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 邮件收件人
    /// </summary>
    public class EmailTarget : TargetInfo
    {
        /// <summary>
        /// 邮件ID
        /// </summary>
        public string Email_Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EmailTargetCategory Category { get; set; } 
    }
}
