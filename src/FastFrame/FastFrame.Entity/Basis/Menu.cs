using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Export]
    [Tree(nameof(Parent_Id))]
    [RelatedField(nameof(Name), nameof(EnCode))]
    public class Menu:BaseEntity
    {  
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(50), Required]
        public string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50),Required]
        public string Name { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [StringLength(50), RelatedTo(typeof(Menu))]
        public string Parent_Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [StringLength(50)]
        public string Path { get; set; }
    }
}
