using FastFrame.Entity.Enums;
using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 部门
    /// </summary>
    [Export]
    [RelatedField(nameof(Name))]
    public class Dept : BaseEntity, ITreeEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(50), Required, Unique]
        public string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        [RelatedTo(typeof(Dept))]
        public string Super_Id { get; set; } 
    } 
}
