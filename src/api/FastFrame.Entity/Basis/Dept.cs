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
        [RelatedTo(typeof(Dept))]
        public string Super_Id { get; set; }

        /// <summary>
        /// 部门代码
        /// </summary>
        [StringLength(50)]
        [ReadOnly]
        public string TreeCode { get; set; } = "保存时生成";

        /// <summary>
        /// 部门名称
        /// </summary>
        [StringLength(50), Required]
        [IsPrimaryField]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string Remarks { get; set; }


        private static Expression<Func<Dept, IViewModel>> vm_expression = v => new DefaultViewModel { Id = v.Id, Value = v.Name + "(" + v.TreeCode + ")" };

        public static Expression<Func<Dept, IViewModel>> BuildExpression()
        {
            return vm_expression;
        }

        public void SetNumber(string val)
        {
            TreeCode = val;
        }

        public string GetNumber() => TreeCode;
    }
}
