using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限定义提供者
    /// </summary>
    public interface IPermissionDefinitionProvider
    {
        Task RegisterPermission(IPermissionDefinitionContext context);
    }
}
