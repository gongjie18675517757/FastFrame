﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 通知
    /// </summary>
    public class Notify:IEntity,IHasTenant
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
        /// 组织
        /// </summary>
        public string Tenant_Id { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
