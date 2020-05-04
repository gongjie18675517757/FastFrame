using CSRedis;
using FastFrame.Application;
using FastFrame.Application.Chat;
using FastFrame.Database;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.MessageBus;
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
    public class ApplicationInitialProvider : IApplicationInitialProvider
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<ApplicationInitialProvider> logger;
        private readonly IMemoryCache memoryCache;

        public ApplicationInitialProvider(IServiceProvider serviceProvider, ILogger<ApplicationInitialProvider> logger, IMemoryCache memoryCache)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// 程序初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitialAsync()
        {
            try
            {
                TypeManger.RegisterBaseType<IEntity>();
                TypeManger.RegisterBaseType<IDto>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "注册类型管理失败！");
            }

            try
            {
                serviceProvider.GetService<RedisClient>();
                serviceProvider.GetService<IMessageBus>().SubscribeAsync<RecMsgOutPut>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "注册Redis失败！");
            }

            try
            {
                await serviceProvider.GetService<DataBase>().Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "数据库迁移失败！");
            }

            try
            {
                memoryCache.Set("Cache:Multi-TenantHost-List", await serviceProvider.GetService<DataBase>().Set<TenantHost>().ToArrayAsync());
                memoryCache.Set("Cache:Multi-Tenant-List", await serviceProvider.GetService<DataBase>().Set<Tenant>().ToArrayAsync());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "缓存多租户信息失败！");
            }

            try
            {
                var permissionDefinitionContext = serviceProvider.GetService<IPermissionDefinitionContext>();
                var permissionProviders = serviceProvider.GetServices<IPermissionProvider>();
                foreach (var permissionProvider in permissionProviders)
                {
                    permissionProvider.RegisterPermission(permissionDefinitionContext);
                }

                var baseType = typeof(BaseController);
                var controllerTypes = baseType.Assembly.GetTypes();

                foreach (var controllerType in controllerTypes)
                {
                    if (!baseType.IsAssignableFrom(controllerType) || controllerType.IsAbstract)
                        continue;

                    /*权限组*/
                    var groupPermissions = controllerType.GetCustomAttributes<PermissionAttribute>();

                    foreach (var groupPermission in groupPermissions)
                    {
                        if (!groupPermission.IsDefinition)
                            continue;

                        var permissionDefinition = permissionDefinitionContext.RegisterPermission(groupPermission.PermissionKey, groupPermission.Text);
                        var methodInfos = controllerType.GetMethods();
                        foreach (var methodInfo in methodInfos)
                        {
                            /*子权限*/
                            var permissions = methodInfo.GetCustomAttributes<PermissionAttribute>();
                            foreach (var permission in permissions)
                            {
                                if (permission.IsDefinition)
                                {
                                    permissionDefinition.RegisterChildPermission(permission.PermissionKey, permission.Text);
                                }
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "注册权限失败！");
            }
        }
    }
}
