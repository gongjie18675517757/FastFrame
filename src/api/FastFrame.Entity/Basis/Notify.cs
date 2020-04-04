using FastFrame.Entity.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 通知
    /// </summary>
    [Export]
    public class Notify : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [EnumItem(nameof(EnumName.NotifyType))]
        public string Type_Id { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        [RelatedTo(typeof(User))]
        public string Publush_Id { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [RelatedTo(typeof(Resource))]
        public string Resource_Id { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [StringLength(8000)]
        [FormGroup("内容")]
        public string Content { get; set; }
    }
}
