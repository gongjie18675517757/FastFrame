using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 部门
    /// </summary>
    [Export]
    public class Dept : BaseEntity, ITreeEntity, IViewModelable<Dept>
    {
        /// <summary>
        /// 上级
        /// </summary>
        [RelatedTo<Dept>]
        public string Super_Id { get; set; }

        /// <summary>
        /// 部门代码
        /// </summary>
        [StringLength(50)]
        [Required]
        [Unique]
        public string EnCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [StringLength(50)]
        [IsPrimaryField]
        [Unique]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string Remarks { get; set; }

        /// <summary>
        /// 树状码
        /// </summary>
        [Exclude]
        public string TreeCode { get; set; }


        private static readonly Expression<Func<Dept, IViewModel>> vm_expression =
            v => new DefaultViewModel
            {
                Id = v.Id,
                Value = v.Name //+ (string.IsNullOrWhiteSpace(v.EnCode) ? "" : "(" + v.EnCode + ")")
            };

        public static Expression<Func<Dept, IViewModel>> BuildExpression() => vm_expression;

        public Expression<Func<Dept, IViewModel>> GetBuildExpression() => vm_expression;
    }
}
