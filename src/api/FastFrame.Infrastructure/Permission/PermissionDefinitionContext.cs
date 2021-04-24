using System.Collections.Generic;
using System.Linq;

namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限定义上下文实现
    /// </summary>
    public class PermissionDefinitionContext : IPermissionDefinitionContext
    {
        private static readonly Dictionary<string, PermissionDefinition> defDic = new Dictionary<string, PermissionDefinition>();

        public IEnumerable<PermissionDefinition> PermissionDefinitions()
        {
            return defDic.Values.Where(v => v.Child.Any());
        }

        public PermissionDefinition TryRegisterPermission(string permissionKey, string permissionText)
        {
            if (!defDic.TryGetValue(permissionKey, out var permissionDefinition))
            {
                permissionDefinition = new PermissionDefinition(permissionKey, permissionText);
                defDic.Add(permissionKey, permissionDefinition);
            }

            return permissionDefinition;
        }
    }
}
