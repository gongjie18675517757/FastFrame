using FastFrame.Infrastructure.Attrs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Dto.Module
{
    public class StrucFieldOutput
    {
        public string Struct_Id { get; set; }

        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string TypeName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 隐藏属性
        /// </summary>
        public HideMark? Hide { get; set; }

        /// <summary>
        /// 只读属性
        /// </summary>
        public ReadOnlyMark? Readonly { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [StringLength(50)]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否长文本
        /// </summary>
        public bool IsTextArea { get; set; }

        /// <summary>
        /// 是否富文本
        /// </summary>
        public bool IsRichText { get; set; }

        /// <summary>
        /// 是否需要必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 关联自
        /// </summary>
        public string RelateName { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        public IEnumerable<RuleOutPut> Rules { get; set; }

        /// <summary>
        /// 枚举属性
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> EnumValues { get; set; }
    }
}
