using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [Exclude]
    public class RolePermission : BaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public string Role_Id { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        [Required]
        public string Permission_Id { get; set; } 
    }
}
