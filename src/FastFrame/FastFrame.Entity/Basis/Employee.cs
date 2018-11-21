using FastFrame.Entity.Enums;
using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 员工
    /// </summary>
    [Export]
    [RelatedField(nameof(Name), nameof(EnCode))]
    public class Employee : BaseEntity
    { 
        /// <summary>
        /// 编码
        /// </summary>
        [Required,StringLength(20)]
        public string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50), EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(20), Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public GenderMark Gender { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [RelatedTo(typeof(User))]
        public string User_Id { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [RelatedTo(typeof(Dept))]
        public string Dept_Id { get; set; }
    } 
}
