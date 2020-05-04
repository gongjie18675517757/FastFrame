using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CSRedis;

namespace FastFrame.Application.Account
{
    public class IdentityManagerService : IService, IIdentityManager
    {
        private readonly IRepository<LoginLog> loginLogRepository;
        private readonly IServiceProvider serviceProvider;
        private readonly CSRedisClient redisClient;

        public IdentityManagerService(IRepository<LoginLog> loginLogRepository, IServiceProvider serviceProvider, CSRedis.CSRedisClient redisClient)
        {
            this.loginLogRepository = loginLogRepository;
            this.serviceProvider = serviceProvider;
            this.redisClient = redisClient;
        }

        public async Task<IIdentity> GenerateIdentity(string userId)
        {
            var loginLog = await loginLogRepository.AddAsync(new LoginLog
            {
                IsEnabled = true,
                ExpiredTime = DateTime.Now.AddDays(1),
                Id = null,
                LastTime = DateTime.Now,
                LoginTime = DateTime.Now,
                User_Id = userId
            });

            await redisClient.SetAsync(loginLog.Id, loginLog, 60 * 60 * 24);

            return loginLog;
        }

        /// <summary>
        /// 刷新Token
        /// </summary> 
        public async Task RefreshTokenAsync(string token)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var loginLogs = serviceScope.ServiceProvider.GetService<IRepository<LoginLog>>();
            var loginLog = await loginLogs.GetAsync(token);
            if (loginLog != null)
            {
                loginLog.ExpiredTime = DateTime.Now.AddDays(1);
                loginLog.LastTime = DateTime.Now;
                await loginLogRepository.UpdateAsync(loginLog);
                await loginLogRepository.CommmitAsync();

                await redisClient.SetAsync(loginLog.Id, loginLog, 60 * 60 * 24);
            }
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> ExistsTokenAsync(string token)
        {
            var dt = DateTime.Now;

            return await redisClient.ExistsAsync(token) ||
                   await loginLogRepository.AnyAsync(v => v.Id == token && v.IsEnabled && v.IsEnabled && dt < v.ExpiredTime);
        }

        /// <summary>
        /// 指定身份失效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task SetTokenFailureAsync(string token)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var loginLogs = serviceScope.ServiceProvider.GetService<IRepository<LoginLog>>();
            var log = await loginLogs.GetAsync(token);
            if (log != null)
            {
                log.ExpiredTime = DateTime.Now;
                log.IsEnabled = false;
                await loginLogRepository.UpdateAsync(log);
                await loginLogRepository.CommmitAsync();

                await redisClient.DelAsync(token);
            }
        }

        /// <summary>
        /// 指定用户全部身份失效
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task SetUserAllTokenFailureAsync(string userId)
        {
            var dt = DateTime.Now;
            var list = await loginLogRepository
                            .Where(v => v.User_Id == userId && v.IsEnabled && v.IsEnabled && dt < v.ExpiredTime)
                            .ToListAsync();

            foreach (var item in list)
            {
                item.IsEnabled = false;
                item.ExpiredTime = dt;

                await loginLogRepository.UpdateAsync(item);
                await redisClient.DelAsync(item.Id);
            }
        }
    }
}
