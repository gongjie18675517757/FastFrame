﻿using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 查询方案明细
    /// </summary> 
    public class QueryProgramDetail:BaseEntity
    {
        /// <summary>
        /// 方案ID
        /// </summary>
        [StringLength(50), Required]
        public string SearchProgram_Id { get; set; }

        /// <summary>
        /// 条件名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 比较操作符
        /// </summary>
        [StringLength(50), Required]
        public string Compare { get; set; }

        /// <summary>
        /// 比较的值
        /// </summary>
        [StringLength(50), Required]
        public string Value { get; set; }
    }
}