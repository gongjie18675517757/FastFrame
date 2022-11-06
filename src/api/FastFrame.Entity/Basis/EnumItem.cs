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
        /// 字段类别
        /// </summary>
        [Required]
        [ReadOnly(ReadOnlyMark.Edit)]
        public EnumName? Key { get; set; }

        /// <summary>
        /// 上级值
        /// </summary>  
        [RelatedTo(typeof(EnumItem))]
        public string Super_Id { get; set; }

        /// <summary>
        /// 树状码
        /// </summary>
        [StringLength(20)]
        [ReadOnly]
        public string TreeCode { get; set; } = "保存时生成";

        /// <summary>
        /// 字典值
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 字典键
        /// </summary>
        public int? IntKey { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortVal { get; set; } 
 

        public void SetNumber(string val)
        {
            TreeCode = val;
        }

        public string GetNumber() => TreeCode;
    }
}
