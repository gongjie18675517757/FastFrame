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
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary> 
        [Required(ErrorMessage = "密码是必填的")]
        public string Password { get; set; } 
    }
}
