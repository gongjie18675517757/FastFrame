using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using System;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 登陆用户
    /// </summary>
    [Export]
    [RelatedField(nameof(Account), nameof(Name))]
    [Unique(nameof(Account))]
    public class User : BaseEntity
    {
        /// <summary>
        /// 帐号
        /// </summary>
        [StringLength(50), Required, Unique]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(50), Required]
        [Hide(HideMark.List)]
        public string Password { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        [StringLength(36), Required, Exclude]
        public string EncryptionKey { get; private set; }

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
        /// 是否超级管理员
        /// </summary>
        [Hide]
        [ReadOnly]
        [Exclude]
        public bool IsRoot { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>      
        [ReadOnly]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 生成密码
        /// </summary>
        /// <param name="password"></param>
        public void GeneratePassword(string password = "")
        {
            if (password.IsNullOrWhiteSpace())
                password = Password;
            EncryptionKey = Guid.NewGuid().ToString().ToMD5();
            Password = $"{EncryptionKey}{password}".ToMD5();
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerificationPassword(string password = "")
        {
            return $"{EncryptionKey}{password}".ToMD5() == Password;
        }
    }
}
