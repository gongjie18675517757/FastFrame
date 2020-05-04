using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限定义模型
    /// </summary>
    public class PermissionDefinition : IEqualityComparer<PermissionDefinition>
    {
        private readonly HashSet<PermissionDefinition> child;

        public PermissionDefinition(string permissionKey, string permissionText)
        {
            PermissionKey = permissionKey;
            PermissionText = permissionText;
            child = new HashSet<PermissionDefinition>(20, this);
        }

        public PermissionDefinition(string permissionKey, string permissionText, IEnumerable<PermissionDefinition> child)
        {
            this.child = new HashSet<PermissionDefinition>(child, this);
            PermissionKey = permissionKey;
            PermissionText = permissionText;
        }

        /// <summary>
        /// 权限键
        /// </summary>
        public string PermissionKey { get; set; }

        /// <summary>
        /// 权限文本
        /// </summary>
        public string PermissionText { get; set; }

        /// <summary>
        /// 子级权限
        /// </summary>
        public IEnumerable<PermissionDefinition> Child { get => child; }

        /// <summary>
        /// 注册子权限
        /// </summary>
        /// <param name="permissionKey"></param>
        /// <param name="permissionText"></param>
        /// <returns></returns>
        public PermissionDefinition RegisterChildPermission(string permissionKey, string permissionText)
        {
            var permissionDefinition = new PermissionDefinition(permissionKey, permissionText);
            child.Add(permissionDefinition);
            return permissionDefinition;
        }

        public bool Equals(PermissionDefinition x, PermissionDefinition y)
        {
            return x?.PermissionKey == y?.PermissionKey;
        }

        public int GetHashCode(PermissionDefinition obj)
        {
            return obj.PermissionKey.GetHashCode();
        }
    }
}
