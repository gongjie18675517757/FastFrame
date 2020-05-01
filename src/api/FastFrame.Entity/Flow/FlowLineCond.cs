using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程线条件
    /// </summary> 
    public class FlowLineCond : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowLink
        /// </summary>
        public string FlowLink_Id { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FieldName { get; set; }

        /// <summary>
        /// 字段文本
        /// </summary> 
        [StringLength(100)]
        [Required]
        public string FieldText { get; set; }

        /// <summary>
        /// 比较方式
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Compare { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FieldValue { get; set; }

        /// <summary>
        /// 值文本
        /// </summary>
        [StringLength(500)]
        public string FieldValueText { get; set; }
    }
}
