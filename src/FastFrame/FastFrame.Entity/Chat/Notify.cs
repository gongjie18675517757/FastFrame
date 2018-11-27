using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 通知
    /// </summary>
    public class Notify
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public string Target_Id { get; set; }

        [NotMapped]
        public string[] TargetUsers { get; set; }

        [NotMapped]
        public string[] FromUsers { get; set; }
    }

    /// <summary>
    /// 消息
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        public string Resource_Id { get; set; }

        public string From_Id { get; set; } 

        public string Target_Id { get; set; }  
    }


    /// <summary>
    /// 邮件
    /// </summary>
    public class Email
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
        public string Replay_Id { get; set; }

        [NotMapped]
        public string FromUser { get; set; }

        [NotMapped]
        public string[] TargetUsers { get; set; }

        [NotMapped]
        public string[] CcUsers { get; set; }
    }

    /// <summary>
    /// 邮件正文
    /// </summary>
    public class EmailContent
    {
        public string Email_Id { get; set; }

        public string Content { get; set; }
    }

    /// <summary>
    /// 邮件附件
    /// </summary>
    public class EmailAnnex
    {
        public string Email_Id { get; set; }

        public string Resource_Id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ChatUser
    {
        /// <summary>
        /// 实例
        /// </summary>
        public string Chat_Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 已读
        /// </summary>
        public bool IsRead { get; set; }
    }
}
