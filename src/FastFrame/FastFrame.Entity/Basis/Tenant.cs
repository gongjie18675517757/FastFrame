using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 组织信息
    /// </summary>
    [Export]
    public class Tenant : IEntity, IHasSoftDelete
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
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 删除码
        /// </summary>
        [Exclude]
        public bool IsDeleted { get; set; }
    }
}
