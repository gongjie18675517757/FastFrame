using FastFrame.Entity.System;
using FastFrame.Infrastructure.Attrs;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Dto
{
    public class UserDto: BaseDto<User>
    {
        /// <summary>
        /// 帐号
        /// </summary>
        [StringLength(50), Required(), Unique]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(50), Required]
        [Hide(HideMark.List)]
        public string Password { get; set; } 

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50), EmailAddress, Unique]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(20), Phone, Unique]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(200)]
        [Hide]
        public string HandIconId { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [ReadOnly]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>      
        [ReadOnly]
        public bool IsDisabled { get; set; }
    }
}
