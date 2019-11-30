﻿using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 图片库
    /// </summary>
    [Export]
    [RelatedField(nameof(Name))]
    [Unique(nameof(Parent_Id), nameof(Name))]
    public class Meidia : BaseEntity
    {
        /// <summary>
        /// 上级
        /// </summary>
        [RelatedTo(typeof(Meidia))]
        public string Parent_Id { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [StringLength(200)]
        public string Href { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        [RelatedTo(typeof(Resource), true)]
        public string Resource_Id { get; set; }

        /// <summary>
        /// 资源标识
        /// </summary>
        [StringLength(50)]
        public string ContentType { get; set; }

        /// <summary>
        /// 是否文件夹
        /// </summary>
        public bool IsFolder { get; set; }
    }
}
