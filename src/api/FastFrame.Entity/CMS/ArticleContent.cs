using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.CMS
{
    /// <summary>
    /// 文章内容
    /// </summary>
    [Exclude]
    public class ArticleContent:IEntity
    {        
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [Hide(HideMark.List)]
        public string Content { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
