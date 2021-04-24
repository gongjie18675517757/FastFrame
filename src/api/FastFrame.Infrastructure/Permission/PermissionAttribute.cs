using System;
using System.Linq;

namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class PermissionAttribute : Attribute
    {
        private readonly string[] permissionKeys;

        /// <summary>
        /// 应用权限
        /// </summary>
        /// <param name="permissionKey"></param>
        public PermissionAttribute(string permissionKey)
        {
            permissionKeys = new string[] { permissionKey };
        }

        /// <summary>
        /// 定义权限
        /// </summary>
        /// <param name="permissionKey"></param>
        /// <param name="text"></param>
        public PermissionAttribute(string permissionKey, string text)
        {
            PermissionKey = permissionKey;
            Text = text;
            IsDefinition = true;
            permissionKeys = Array.Empty<string>();
        }

        /// <summary>
        /// 应用权限
        /// </summary>
        /// <param name="permissionKeys"></param>
        public PermissionAttribute(string[] permissionKeys)
        {
            IsDefinition = false;
            this.permissionKeys = permissionKeys;
        }

        /// <summary>
        /// 是否权限定义
        /// </summary>
        public bool IsDefinition { get; } = false;

        /// <summary>
        /// 权限标记
        /// </summary>
        public string PermissionKey { get; }

        /// <summary>
        /// 权限文本
        /// </summary>
        public string Text { get; }


        public string[] PermissionKeys => permissionKeys.Length > 0 ? permissionKeys : new string[] { PermissionKey };
    }
}
