using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 部门成员
    /// </summary>
    [Infrastructure.Attrs.Exclude]
    public class DeptMember : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [Required]
        public string Dept_Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary> 
        [Required]
        public string User_Id { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsManager { get; set; }
    }
}
