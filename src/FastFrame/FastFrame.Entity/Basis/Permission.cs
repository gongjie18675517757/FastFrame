using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 权限
    /// </summary> 
    [Export]
    [Tree(nameof(Parent_Id))]
    [RelatedField(nameof(Description), nameof(Name), nameof(AreaName))]
    public class Permission : BaseEntity
    {
        /// <summary>
        /// 父级
        /// </summary>
        [RelatedTo(typeof(Permission))]
        public string Parent_Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [StringLength(50), Required]
        public string AreaName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(50), Required]
        public string Description { get; set; }
    }
}
