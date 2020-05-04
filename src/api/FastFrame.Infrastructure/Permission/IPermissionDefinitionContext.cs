using System.Collections.Generic;

namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限定义上下文接口
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// 注册权限
        /// </summary>
        /// <param name="permissionKey"></param>
        /// <param name="permissionText"></param>
        /// <returns></returns>
        PermissionDefinition RegisterPermission(string permissionKey, string permissionText);

        /// <summary>
        /// 所有已注册的权限
        /// </summary>
        /// <returns></returns>
        IEnumerable<PermissionDefinition> PermissionDefinitions();
    }
}
