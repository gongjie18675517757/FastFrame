using System;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 权限描述
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class PermissionAttribute : System.Attribute
    {
        public PermissionAttribute(string name,string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; }
    }
}
