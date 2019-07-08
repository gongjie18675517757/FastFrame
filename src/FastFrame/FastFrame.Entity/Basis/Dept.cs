using FastFrame.Entity.Enums;
using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 部门
    /// </summary>
    [Export]
    [RelatedField(nameof(Name), nameof(EnCode))]
    [Tree(nameof(Super_Id))]
    public class Dept : BaseEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(50), Required, Unique]
        public string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; } 

        /// <summary>
        /// 主管
        /// </summary>
        [StringLength(50), RelatedTo(typeof(User))]
        public string Supervisor_Id { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        [StringLength(50), RelatedTo(typeof(Dept))]
        public string Super_Id { get; set; }
    }
}
