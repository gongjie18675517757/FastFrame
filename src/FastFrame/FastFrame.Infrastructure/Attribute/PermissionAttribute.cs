using System;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 权限描述
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class PermissionAttribute : System.Attribute
    {
        public PermissionAttribute(string name, string description)
        {
            Name = name;
            Description = description;
            IsPrimary = true;
        }
        public PermissionAttribute(params string[] anOtherNames)
        {
            AnOtherNames = anOtherNames;
            IsPrimary = false;
        }

        public bool IsPrimary { get; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; }
        public string[] AnOtherNames { get; }
    }
}
