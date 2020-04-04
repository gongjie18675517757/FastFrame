using FastFrame.Entity.Enums;
using FastFrame.Infrastructure.Attrs;
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
        public EnumName Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        [RelatedTo(typeof(EnumItem))]
        public string Super_Id { get; set; }
    }
}
