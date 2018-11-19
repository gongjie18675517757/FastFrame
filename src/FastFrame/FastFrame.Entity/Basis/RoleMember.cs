using FastFrame.Infrastructure.Attrs;

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
        public string Role_Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string User_Id { get; set; }
    }
}
