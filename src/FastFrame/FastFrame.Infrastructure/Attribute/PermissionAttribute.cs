using System;
using System.Linq;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 权限描述
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class PermissionAttribute : System.Attribute
    {
        public PermissionAttribute(string encode, string name)
        {
            EnCode = encode;
            Name = name;
            IsPrimary = true;
        }
        public PermissionAttribute()
        {
            IsPrimary = false;
        }

        public bool IsPrimary { get; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string EnCode { get; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 别名
        /// </summary>
        public string[] AnOtherEnCodes { get; set; } = new string[0];


        public string[] AllEnCodes => AnOtherEnCodes.Concat(new[] { EnCode }).ToArray();
    }
}
