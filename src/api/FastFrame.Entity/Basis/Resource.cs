﻿using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 资源
    /// </summary>     
    [Infrastructure.Attrs.RelatedField(nameof(Name))]
    public class Resource : IEntity, IHasTenant
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// 资源大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        [StringLength(150)]
        public string Path { get; set; }

        /// <summary>
        /// 资源标识
        /// </summary>
        [StringLength(50)]
        public string ContentType { get; set; }

        /// <summary>
        /// MD5摘要
        /// </summary>
        public string MD5 { get; set; }

        public string Id { get; set; }
    }
}
