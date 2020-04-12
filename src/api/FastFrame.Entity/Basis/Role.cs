using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色
    /// </summary>
    [RelatedField(nameof(Name), nameof(EnCode))]
    [Export]
    public class Role : BaseEntity
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
    }
}
