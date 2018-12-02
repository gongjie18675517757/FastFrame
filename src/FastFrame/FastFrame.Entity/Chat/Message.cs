using System;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 消息
    /// </summary>
    public abstract class Message : IEntity, IHasTenant
    {
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType Category { get; set; }

        /// <summary>
        /// 图片?附件ID
        /// </summary>
        public string Resource_Id { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public string From_Id { get; set; }

        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        public string Tenant_Id { get; set; }
    }
}
