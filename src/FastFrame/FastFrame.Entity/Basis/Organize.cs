using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 组织信息
    /// </summary>
    [Export]
    public class Organize : BaseEntity
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; } 
    }
}
