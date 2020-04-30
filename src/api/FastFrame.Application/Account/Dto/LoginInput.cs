using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFrame.Application.Account
{
    public class LoginInput
    {
        /// <summary>
        /// 帐号
        /// </summary>
        [Required(ErrorMessage = "帐号是必填的")]
        [StringLength(20, MinimumLength = 4,ErrorMessage ="帐号的长度要求5-20个字")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码的长度要求6-20个字")]
        [Required(ErrorMessage = "密码是必填的")]
        public string Password { get; set; } 
    }
}
