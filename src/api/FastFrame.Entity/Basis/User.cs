using FastFrame.Entity.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 用户
    /// </summary>
    [Export]
    [RelatedField(nameof(Name), nameof(Account))]
    [Unique(nameof(Account))]
    public class User : BaseEntity
    {
        public const int NameLength = 50;



        /// <summary>
        /// 帐号
        /// </summary>
        [StringLength(50), Required, Unique]
        [ReadOnly(ReadOnlyMark.Edit)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(50), Required]
        [Hide(HideMark.List)]
        [ReadOnly(ReadOnlyMark.Edit)]
        public string Password { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        [StringLength(36), Required, Exclude]
        public string EncryptionKey { get; private set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(NameLength), Required]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50), Unique]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(20), Unique]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(200)]
        [RelatedTo(typeof(Resource))]
        [Hide(HideMark.Form)]
        public string HandIcon_Id { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [ReadOnly]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        [ReadOnly]
        public EnabledMark Enable { get; set; } = EnabledMark.enabled;

        /// <summary>
        /// 生成密码
        /// </summary> 
        public void GeneratePassword(string password = "")
        {
            if (string.IsNullOrEmpty(password))
                password = Password;

            EncryptionKey = ToMD5(Guid.NewGuid().ToString());
            Password = ToMD5($"{EncryptionKey}{password}");
        }

        /// <summary>
        /// 验证密码
        /// </summary> 
        public bool VerificationPassword(string password = "")
        {
            return ToMD5($"{EncryptionKey}{password}") == Password;
        }


        private static string ToMD5(string @in)
        {
            if (@in == null)
                return null;

            using var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.Default.GetBytes(@in));
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", "").ToLower();
        }
    }
}
