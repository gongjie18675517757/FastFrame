using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Chat
{
    /// <summary>
    /// 通知
    /// </summary>
    [Export]
    public class Notify:BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required] 
        public string Content { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        [RelatedTo(typeof(Employee))]
        public string Publush_Id { get; set; }
    }
}
