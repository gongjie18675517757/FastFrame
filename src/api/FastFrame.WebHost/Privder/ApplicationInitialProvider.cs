using FastFrame.Application;
using FastFrame.Database;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Module;
using FastFrame.Infrastructure.Permission;
using FastFrame.WebHost.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    /// <summary>
    /// 应用程序初始化服务
    /// </summary>
    public class ApplicationInitialProvider(IServiceProvider serviceProvider,
                                            ILogger<ApplicationInitialProvider> logger,
                                            IMemoryCache memoryCache) 
        : IApplicationInitialLifetime
    {

        /// <summary>
        /// 程序初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitialAsync()
        {
            /*注册类型*/
            try
            {
                TypeManger.RegisterBaseType<IEntity>();
                TypeManger.RegisterBaseType<IDto>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "注册类型管理失败！");
            }

            /*数据库迁移*/
            try
            {
                await serviceProvider.GetService<DataBase>().Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "数据库迁移失败！");
            }

            /*缓存多租户信息*/
            try
            {
                memoryCache.Set(ConstValuePool.CacheTenantHost, await serviceProvider.GetService<DataBase>().Set<TenantHost>().ToArrayAsync());
                memoryCache.Set(ConstValuePool.CacheTenant, await serviceProvider.GetService<DataBase>().Set<Tenant>().ToArrayAsync());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "缓存多租户信息失败！");
            }

            /*注册权限*/
            try
            {
                var permissionDefinitionContext = serviceProvider.GetService<IPermissionDefinitionContext>();
                var permissionProviders = serviceProvider.GetServices<IPermissionDefinitionProvider>();
                var moduleDesProvider = serviceProvider.GetService<IModuleDesProvider>();
                foreach (var permissionProvider in permissionProviders)
                {
                    await permissionProvider.RegisterPermission(permissionDefinitionContext);
                }

                var baseType = typeof(BaseController);
                var controllerTypes = baseType.Assembly.GetTypes();

                foreach (var controllerType in controllerTypes)
                {
                    if (!baseType.IsAssignableFrom(controllerType) || controllerType.IsAbstract)
                        continue;

                    /*权限组*/
                    var permissionGroupAttribute = controllerType.GetCustomAttribute<PermissionGroupAttribute>();
                    var groupName = permissionGroupAttribute?.Name;
                    var groupText = permissionGroupAttribute?.Text;

                    if (permissionGroupAttribute == null)
                    {
                        groupName = controllerType.Name.Replace("Controller", "");
                        groupText = moduleDesProvider.GetClassDescription(controllerType);
                    }

                    var permissionDefinition = permissionDefinitionContext.TryRegisterPermission(groupName, groupText);
                    var methodInfos = controllerType.GetMethods();
                    foreach (var methodInfo in methodInfos)
                    {
                        /*权限标记*/
                        var permissions = methodInfo.GetCustomAttributes<PermissionAttribute>();
                        foreach (var permission in permissions)
                        {
                            if (permission.IsDefinition)
                            {
                                var permissionName = permission.PermissionKey;
                                if (!permissionName.Contains('.'))
                                    permissionName = $"{groupName}.{permissionName}";

                                permissionDefinition.RegisterChildPermission(permissionName, permission.Text);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "注册权限失败！");
            }

            /*注册模块*/
            try
            {
                var moduleDefinitionProviders = serviceProvider.GetServices<IModuleDefinitionProvider>();
                var moduleExportProvider = serviceProvider.GetService<IModuleExportProvider>();
                foreach (var item in moduleDefinitionProviders)
                {
                    moduleExportProvider.RegisterModule(await item.DefinitionModuleAsync());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "注册模块失败！");
            }
        }
    }
}
