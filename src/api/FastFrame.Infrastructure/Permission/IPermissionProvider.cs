namespace FastFrame.Infrastructure.Permission
{
    /// <summary>
    /// 权限定义提供者
    /// </summary>
    public interface IPermissionProvider
    {
        void RegisterPermission(IPermissionDefinitionContext context);
    }
}
