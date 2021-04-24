using System;

namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限组标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PermissionGroupAttribute : Attribute
    {
        public PermissionGroupAttribute(string name,string text)
        {
            Name = name;
            Text = text;
        }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 权限组文本
        /// </summary>
        public string Text { get; }
    }
}
