using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限检查者
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// 检查是否被授权
        /// </summary>
        /// <param name="groupPermissionKey"></param>
        /// <param name="childPermissionKeys"></param>
        /// <returns></returns>
        Task<bool> CheckIsGrantedAsync(string groupPermissionKey, string[] childPermissionKeys);

        /// <summary>
        /// 所有被授权的权限
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PermissionDefinition>> GetGrantedPermissions(); 
    }
}
