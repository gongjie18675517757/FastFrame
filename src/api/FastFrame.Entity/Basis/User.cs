using FastFrame.Entity.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 用户
    /// </summary>
    [Export]
    [Unique(nameof(Account))]
    public class User : BaseEntity, IViewModelable<User> //, IVerifyIdentity
    {
        public const int NameLength = 50;

        /// <summary>
        /// 帐号
        /// </summary>
        [StringLength(50), Required, Unique]
        [ReadOnly(ReadOnlyMark.Edit)]
        [IsPrimaryField]
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
        [RelatedTo(typeof(Resource))]
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
        [EnumItem(EnumName.EnabledMark)]
        public int Enable { get; set; } = (int)EnabledMark.enabled;

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
        public bool VerificationPassword(string password, out Exception exception)
        {
            exception = null;
            if (Enable == (int)EnabledMark.disabled)
            {
                exception = new Exception("帐号已禁用");
                return false;
            }

            if (ToMD5($"{EncryptionKey}{password}") != Password)
            {
                exception = new Exception("帐号密码不正确!");
                return false;
            }

            return true;
        }


        private static string ToMD5(string @in)
        {
            if (@in == null)
                return null;
            var result = MD5.HashData(Encoding.Default.GetBytes(@in));
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", "").ToLower();
        }



        private static readonly Expression<Func<User, IViewModel>> vm_expression =
            v => new DefaultViewModel { Id = v.Id, Value = v.Name + "(" + v.Account + ")" };

        public static Expression<Func<User, IViewModel>> BuildExpression() => vm_expression;

        public Expression<Func<User, IViewModel>> GetBuildExpression() => vm_expression;
    }
}
