﻿using FastFrame.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 通知
    /// </summary>
    [Export]
    public class Notify : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [EnumItem(EnumName.NotifyType)]
        public int? Type_Id { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        [RelatedTo<User>]
        public string Publush_Id { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [RelatedTo<Resource>]
        public string Resource_Id { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required] 
        [FormGroup("内容")]
        public string Content { get; set; }
    }
}
