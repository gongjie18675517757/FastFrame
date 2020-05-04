using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色权限
    /// </summary> 
    public class RolePermission : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary> 
        public string Role_Id { get; set; }

        /// <summary>
        /// 权限标记
        /// </summary>
        [StringLength(100)]
        public string PermissionKey { get; set; }

        /// <summary>
        /// 上级权限标记
        /// </summary>
        [StringLength(100)]
        public string SuperPermissionKey { get; set; }
    }
}
