using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Identity;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.IntervalWork;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FastFrame.Application.Account
{
    public class IdentityManagerService : IService, IIdentityManager
    { 
        private readonly IRepository<LoginLog> loginLogRepository;
        private readonly IOptionsMonitor<IdentityConfig> optionsMonitor;
        private readonly IBackgroundJob backgroundJob;

        public class Identity : LoginLog, IIdentity
        {
            public string GetToken() => Id;
        }

        public IdentityManagerService(IRepository<LoginLog> loginLogRepository, IOptionsMonitor<IdentityConfig> optionsMonitor, IBackgroundJob backgroundJob)
        {
            this.loginLogRepository = loginLogRepository;
            this.optionsMonitor = optionsMonitor;
            this.backgroundJob = backgroundJob;
        }

        /// <summary>
        /// 验证失败次数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<MsgException> VerifyFailCount(string userId, IPAddress address)
        {
            MsgException ex = null;
            var identityConfig = optionsMonitor.CurrentValue;
            if (userId != null)
            {
                var time = DateTime.Now.Add(-identityConfig.FailTime);
                var c = await loginLogRepository.CountAsync(v => v.User_Id == userId && v.LoginTime >= time && !v.IsSuccessful);

                if (c <= identityConfig.FailCount)
                    return null;

                ex = new MsgException($"帐号已锁定:最近{identityConfig.FailTime}内,连续{c}次登录失败");
            }
            return ex;
        }

        public async Task InsertLog(Identity identity)
        {
            await loginLogRepository.AddAsync(identity);
            await loginLogRepository.CommmitAsync();
        }

        /// <summary>
        /// 尝试生成身份
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="address"></param>
        /// <param name="password"></param>
        /// <param name="verifyIdentity"></param> 
        /// <returns></returns>
        public async Task<IIdentity> TryGenerateIdentity(string userId,
                                 IPAddress address,
                                 string password,
                                 IIdentityManager.VerifyIdentity verifyIdentity)
        {
            Exception ex = null;
            Identity identity = null;
            /*验证登录次数*/
            MsgException msgException = await VerifyFailCount(userId, address);

            /*验证帐号密码*/
            if (msgException == null && verifyIdentity != null && userId != null && verifyIdentity.Invoke(password, out ex))
            {
                identity = new Identity
                {
                    IsEnabled = true,
                    ExpiredTime = DateTime.Now.Add(optionsMonitor.CurrentValue.TokenEffectiveTime),
                    Id = IdGenerate.NetId(),
                    LastTime = DateTime.Now,
                    LoginTime = DateTime.Now,
                    User_Id = userId,
                    IPAddress = address?.ToString(),
                    IsSuccessful = true,
                    FailReason = null,
                };
            }
            else if (ex != null)
            {
                msgException = new MsgException(ex.Message);
            }

            /*失败log*/
            if (identity == null)
                identity = new Identity
                {
                    IsEnabled = false,
                    ExpiredTime = null,
                    Id = IdGenerate.NetId(),
                    LastTime = null,
                    LoginTime = DateTime.Now,
                    User_Id = userId,
                    IPAddress = address?.ToString(),
                    IsSuccessful = false,
                    FailReason = msgException?.Message,
                };

            backgroundJob.SetTimeout<IdentityManagerService>(v => v.InsertLog(identity), null);

            if (msgException != null)
                throw msgException;

            return identity;
        } 

        public async Task RefreshTokenAsync(string token)
        {
            var loginLog = await loginLogRepository.GetAsync(token);
            if (loginLog != null)
            {
                loginLog.ExpiredTime = DateTime.Now.Add(optionsMonitor.CurrentValue.TokenEffectiveTime);
                loginLog.LastTime = DateTime.Now;
                await loginLogRepository.UpdateAsync(loginLog);
                await loginLogRepository.CommmitAsync();
            }
        }

        /// <summary>
        /// 刷新Token
        /// </summary> 
        public void RefreshToken(string token)
        {
            backgroundJob.SetTimeout<IdentityManagerService>(v => v.RefreshTokenAsync(token), null);
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<bool> ExistsTokenAsync(string token, IPAddress address)
        {
            var ip = address?.ToString();
            var dt = DateTime.Now;
            return await loginLogRepository
                .AnyAsync(v => v.Id == token && v.IsEnabled && v.IsEnabled && dt < v.ExpiredTime && v.IPAddress == ip);
        }

        /// <summary>
        /// 指定身份失效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task SetTokenFailureAsync(string token)
        {
            var log = await loginLogRepository.GetAsync(token);
            if (log != null)
            {
                log.ExpiredTime = DateTime.Now;
                log.IsEnabled = false;
                await loginLogRepository.UpdateAsync(log);
                await loginLogRepository.CommmitAsync();
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
            }

            await loginLogRepository.CommmitAsync();
        }

        public void SetTokenFailure(string token)
        {
            backgroundJob.SetTimeout<IdentityManagerService>(v => v.SetTokenFailureAsync(token), null);
        }

        public void SetUserAllTokenFailure(string userId)
        {
            backgroundJob.SetTimeout<IdentityManagerService>(v => v.SetUserAllTokenFailureAsync(userId), null);
        }
    }
}
