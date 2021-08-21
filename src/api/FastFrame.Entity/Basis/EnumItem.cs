using FastFrame.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 数字字典
    /// </summary>
    [RelatedField(nameof(Value))] 
    [Export]
    public class EnumItem : BaseEntity, ITreeEntity
    {
        /// <summary>
        /// 键
        /// </summary>
        [Required]
        [ReadOnly(ReadOnlyMark.Edit)]
        public EnumName? Key { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(20)]
        [ReadOnly(ReadOnlyMark.Edit)]
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 上级
        /// </summary>  
        [RelatedTo(typeof(EnumItem))]
        public string Super_Id { get; set; }

        /// <summary>
        /// 子节点数量
        /// </summary>
        [ReadOnly]
        public int ChildCount { get; set; }
    }
}
