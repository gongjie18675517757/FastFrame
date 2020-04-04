using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 权限
    /// </summary> 
    [Export] 
    [RelatedField(nameof(Name))]
    public class Permission : IEntity, IHasTenant, ITreeEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

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
        /// 父级
        /// </summary>
        [RelatedTo(typeof(Permission))]
        public string Super_Id { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
