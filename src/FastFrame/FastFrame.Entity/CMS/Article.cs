using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.CMS
{
    /// <summary>
    /// 文章
    /// </summary>
    [Export]
    public class Article:BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 文章类别
        /// </summary>
        [RelatedTo(typeof(ArticleCategory))]
        public string ArticleCategory_Id { get; set; }

        /// <summary>
        /// URL标识
        /// </summary>
        [Unique]
        [Required]
        [StringLength(50)]
        public string Url { get; set; }

        /// <summary>
        /// 概述
        /// </summary>
        [StringLength(50)]
        public string Summarize { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        [RelatedTo(typeof(Meidia))]
        [Hide(HideMark.List)]
        public string Thumbnail_Id { get; set; } 

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        [Hide(HideMark.List)]
        public string Content { get; set; }

        /// <summary>
        /// 发布?
        /// </summary>
        public bool IsRelease { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [ReadOnly]
        [Hide(HideMark.Form)]
        public int LookCount { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }
    } 
}
