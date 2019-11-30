using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 邮件
    /// </summary>
    public class Email : IEntity, IHasTenant
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 回复自
        /// </summary>
        public string Replay_Email_Id { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string FromUser_Id { get; set; } 

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
