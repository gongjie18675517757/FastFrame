using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 组织信息
    /// </summary>
    [Export]
    public class Organize : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(50)]
        [Required]
        public string EnCode { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        [StringLength(200)]
        [Required]
        public string Host { get; set; } 
    }
}
