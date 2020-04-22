﻿using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 编号设置
    /// </summary>
    [Export]
    public class NumberOption : BaseEntity
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [StringLength(100)]
        [Required]
        [Unique]
        public string BeModule { get; set; } 

        /// <summary>
        /// 前缀
        /// </summary>
        [StringLength(10)]
        public string Prefix { get; set; }

        /// <summary>
        /// 是否取日期
        /// </summary>
        public bool TaskDate { get; set; }

        /// <summary>
        /// 流水号长度
        /// </summary>
        public int SerialLength { get; set; } = 3;

        /// <summary>
        /// 后缀
        /// </summary>
        [StringLength(10)]
        public string Suffix { get; set; }

        /// <summary>
        /// 取日期字段
        /// </summary>
        [StringLength(50)]
        public string DateField { get; set; }

        /// <summary>
        /// 日期字段名称
        /// </summary>
        [StringLength(50)]
        public string DateFieldText { get; set; }

        /// <summary>
        /// 日期格式方法
        /// </summary> 
        public FmtDateEnum FmtDate { get; set; } 
    }

    /// <summary>
    /// 日期格式方式
    /// </summary>
    public enum FmtDateEnum
    {
        /// <summary>
        /// 年编(长)
        /// </summary>
        yyyy,

        /// <summary>
        /// 月编(长)
        /// </summary>
        yyyyMM,

        /// <summary>
        /// 日编(长)
        /// </summary>
        yyyyMMdd,

        /// <summary>
        /// 年编(短)
        /// </summary>
        yy,

        /// <summary>
        /// 月编(短)
        /// </summary>
        yyMM,

        /// <summary>
        /// 日编(短)
        /// </summary>
        yyMMdd,
    }

    /// <summary>
    /// 编号记录
    /// </summary>
    [Exclude]
    public class NumberRecord : IEntity,IHasSoftDelete,IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [StringLength(50)]
        public string BeModule { get; set; }

        /// <summary>
        /// 年
        /// </summary> 
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 流水
        /// </summary>
        public int Serial { get; set; }
    }

    /// <summary>
    /// 标识需要编号
    /// </summary>
    public interface IHaveNumber : IEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        string Number { get; } 
    }
}
