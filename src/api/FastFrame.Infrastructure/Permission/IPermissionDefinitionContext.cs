using System.Collections.Generic;

namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限定义上下文接口
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// 尝试注册权限
        /// 如果注册过，则返回之前的
        /// </summary>
        /// <param name="permissionKey"></param>
        /// <param name="permissionText"></param>
        /// <returns></returns>
        PermissionDefinition TryRegisterPermission(string permissionKey, string permissionText);

        /// <summary>
        /// 所有已注册的权限
        /// </summary>
        /// <returns></returns>
        IEnumerable<PermissionDefinition> PermissionDefinitions();


    }
}
