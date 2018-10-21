using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFrame.Entity.System
{
    /// <summary>
    /// 表外键信息
    /// </summary>
    public class Foreign : BaseEntity
    {
        /// <summary>
        /// 数据行ID
        /// </summary>
        [Required]
        public string EntityId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        [RelatedTo(typeof(User))]
        public string CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [RelatedTo(typeof(User))]
        public string ModifyUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
    }
}
