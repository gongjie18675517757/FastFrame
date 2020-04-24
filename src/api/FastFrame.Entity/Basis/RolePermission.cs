using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色权限
    /// </summary> 
    public class RolePermission : IEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary> 
        public string Role_Id { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary> 
        public string Permission_Id { get; set; }

        public string Id { get; set; }
    }
}
