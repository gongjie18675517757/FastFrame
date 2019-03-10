using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色成员
    /// </summary>
    [Exclude]
    public class RoleMember : BaseEntity
    {
        /// <summary>
        /// 角色
        /// </summary>
        [Required]
        public string Role_Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [Required]
        public string User_Id { get; set; }
    }
}
