using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色
    /// </summary> 
    [Export]
    public class Role : BaseEntity, IViewModelable<Role>
    {
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(50), Required, Unique]
        [IsPrimaryField]
        public string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 缺省角色
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 管理员角色
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remarks { get; set; }


        private static Expression<Func<Role, IViewModel>> vm_expression = v => new DefaultViewModel { Id = v.Id, Value = v.Name + "(" + v.EnCode + ")" };

        public static Expression<Func<Role, IViewModel>> BuildExpression() => vm_expression;
    }
}
