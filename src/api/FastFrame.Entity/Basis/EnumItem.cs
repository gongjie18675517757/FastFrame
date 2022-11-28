using FastFrame.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 数字字典
    /// </summary> 
    [Export]
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
        /// 树状码
        /// </summary>
        [StringLength(20)]
        [ReadOnly]
        [IsPrimaryField]
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

        public string GetModuleName()
        {
            return $"nameof(EnumItem):{Key}";
        }

        public string GetNumber() => TreeCode;


        private static Expression<Func<EnumItem, IViewModel>> vm_expression =
                        v => new DefaultViewModel
                        {
                            Id = v.Id,
                            Value = v.Value + (string.IsNullOrWhiteSpace(v.TreeCode) ? "" : "(" + v.TreeCode + ")")
                        };

        public static Expression<Func<EnumItem, IViewModel>> BuildExpression() => vm_expression;
    }
}
