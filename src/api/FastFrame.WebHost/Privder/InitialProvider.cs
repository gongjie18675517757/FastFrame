using CSRedis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Infrastructure.MessageBus;
using FastFrame.Dto.Dtos.Chat;
using FastFrame.Database;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;

namespace FastFrame.WebHost.Privder
{
    /// <summary>
    /// 初始化服务
    /// </summary>
    public class InitialProvider : IApplicationInitialProvider
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<InitialProvider> logger;

        public InitialProvider(IServiceProvider serviceProvider, ILogger<InitialProvider> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }
        public async Task InitialAsync()
        {
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
                var memoryCache = serviceProvider.GetService<IMemoryCache>();
                memoryCache.Set("Cache:Multi-TenantHost-List", await serviceProvider.GetService<DataBase>().Set<TenantHost>().ToArrayAsync());
                memoryCache.Set("Cache:Multi-Tenant-List", await serviceProvider.GetService<DataBase>().Set<Tenant>().ToArrayAsync());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "缓存多租户信息失败！");
            }
        }
    } 
}
