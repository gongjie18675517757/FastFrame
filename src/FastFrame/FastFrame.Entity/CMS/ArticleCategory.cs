using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.CMS
{
    /// <summary>
    /// 文章类别
    /// </summary>
    [Export]
    [Tree(nameof(Parent_Id))]
    [RelatedField(nameof(Name))]
    public class ArticleCategory : BaseEntity
    {
        /// <summary>
        /// 上级标题
        /// </summary>
        [RelatedTo(typeof(ArticleCategory))]
        public string Parent_Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50, MinimumLength = 5)]
        [Required]
        [Unique]
        public string Name { get; set; }

        /// <summary>
        /// Url标识
        /// </summary>
        [StringLength(50, MinimumLength = 2)]
        [Unique]      
        public string Url { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500, MinimumLength = 2)]
        public string Description { get; set; }
    }
}
