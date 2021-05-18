using FastFrame.Entity.Enums;
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
}
