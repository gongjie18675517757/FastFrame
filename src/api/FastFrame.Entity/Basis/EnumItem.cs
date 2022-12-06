using FastFrame.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 数字字典
    /// </summary> 
    [Export]
    [Unique(nameof(Key), nameof(IntKey))]
    [Unique(nameof(Key), nameof(Super_Id), nameof(Value))]
    public class EnumItem : BaseEntity, ITreeEntity, IViewModelable<EnumItem>
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
        /// 字典值
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// 字典键
        /// </summary>
        [Required]
        public int? IntKey { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortVal { get; set; }

        private static Expression<Func<EnumItem, IViewModel>> vm_expression =
                        v => new DefaultViewModel
                        {
                            Id = v.Id,
                            Value = v.Value
                        };

        public static Expression<Func<EnumItem, IViewModel>> BuildExpression() => vm_expression;

        public Expression<Func<EnumItem, IViewModel>> GetBuildExpression() => vm_expression;

        /// <summary>
        /// 树状码
        /// </summary>
        [Exclude]
        public string TreeCode { get; set; }
    }
}
