using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 权限
    /// </summary> 
    [Export]
    [Tree(nameof(Parent_Id))]
    [RelatedField(nameof(Name), nameof(EnCode), nameof(AreaName))]
    public class Permission : IEntity,IHasTenant
    {
        /// <summary>
        /// 父级
        /// </summary>
        [RelatedTo(typeof(Permission))]
        public string Parent_Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(50), Required]
        public string EnCode { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [StringLength(50), Required]
        public string AreaName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Exclude]
        public string Tenant_Id { get; set; }
    }
}
