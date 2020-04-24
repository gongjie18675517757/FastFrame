using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色成员
    /// </summary> 
    public class RoleMember : IEntity
    {
        /// <summary>
        /// 角色
        /// </summary> 
        public string Role_Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary> 
        public string User_Id { get; set; }

        public string Id { get; set; }
    }
}
