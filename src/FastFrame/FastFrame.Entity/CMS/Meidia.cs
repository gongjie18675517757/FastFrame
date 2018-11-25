using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.CMS
{
    /// <summary>
    /// 图片库
    /// </summary>
    [Export]
    [RelatedField(nameof(Name))]
    public class Meidia:BaseEntity
    {
        /// <summary>
        /// 链接
        /// </summary>
        [StringLength(200)]
        public string Href { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; } 
    }
}
