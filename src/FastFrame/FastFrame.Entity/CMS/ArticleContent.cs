using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.CMS
{
    /// <summary>
    /// 文章内容
    /// </summary>
    [Exclude]
    public class ArticleContent:BaseEntity
    {        
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [Hide(HideMark.List)]
        public string Content { get; set; }
    }
}
