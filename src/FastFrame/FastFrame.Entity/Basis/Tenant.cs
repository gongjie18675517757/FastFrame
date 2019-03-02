using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 组织信息
    /// </summary>
    [Export]
    public class Tenant : IEntity, IHasSoftDelete, ITenant
    {
        /// <summary>
        /// 全称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ShortName { get; set; }

        /// <summary>
        /// URL标识
        /// </summary>
        [Unique]
        [StringLength(50)]
        public string UrlMark { get; set; } = "";

        /// <summary>
        /// 上级
        /// </summary>
        public string Parent_Id { get; set; }

        /// <summary>
        /// 是否可发展下级
        /// </summary>
        public bool CanHaveChildren { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HandIcon_Id { get; set; }

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
