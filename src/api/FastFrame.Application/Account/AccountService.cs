using FastFrame.Application.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Account
{
    public class AccountService : IService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<LoginLog> loginLogs;
        private readonly IAppSessionProvider appSession;

        public AccountService(IRepository<User> userRepository, IRepository<LoginLog> loginLogs, IAppSessionProvider appSession)
        {
            this.userRepository = userRepository;
            this.loginLogs = loginLogs;
            this.appSession = appSession;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CurrUser> LoginAsync(LoginInput input)
        {
            var user = await userRepository.Queryable.Where(x => x.Account == input.Account).FirstOrDefaultAsync();
            if (user?.VerificationPassword(input.Password) == true)
            {
                if (user.Enable == EnabledMark.Disabled)
                    throw new MsgException("帐号已被停用");

                var loginLog = await loginLogs.AddAsync(new LoginLog
                {
                    Enable = EnabledMark.Enabled,
                    ExpiredTime = DateTime.Now.AddDays(1),
                    Id = null,
                    LastTime = DateTime.Now,
                    LoginTime = DateTime.Now,
                    User_Id = user.Id
                });

                var curr = new CurrUser()
                {
                    IsAdmin = user.IsAdmin,
                    ToKen = loginLog.Id,
                    Id = user.Id,
                    Name = user.Name,
                    Account = user.Account
                };

                await appSession.LoginAsync(curr);
                return curr;
            }
            throw new MsgException("登陆失败,帐号或者密码错误!");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserDto> RegistAsync(UserDto input)
        {
            var user = input.MapTo<UserDto, User>();
            user.GeneratePassword();
            if (!await userRepository.Queryable.AnyAsync())
            {
                user.IsAdmin = true;
            }
            user = await userRepository.AddAsync(user);
            await userRepository.CommmitAsync();
            return user.MapTo<User, UserDto>();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary> 
        /// <returns></returns>
        public async Task<UserDto> UpdateUserInfo(UserDto input)
        {
            var userId = appSession.CurrUser?.Id;
            var user = await userRepository.GetAsync(userId);
            new
            {
                input.Name,
                input.PhoneNumber,
                input.Email,
                input.HandIcon_Id,
            }.MapSet(user);
            if (input.Password != user.Password)
            {
                user.Password = input.Password;
                user.GeneratePassword();
            }
            await userRepository.UpdateAsync(user);
            await userRepository.CommmitAsync();
            await RedisHelper.DelAsync(userId);
            return await GetCurrentAsync();
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto> GetCurrentAsync()
        {
            var curr = appSession.CurrUser;
            if (curr == null)
                throw new MsgException("未登陆吧!");
            var user = await userRepository.GetAsync(curr?.Id);
            await appSession.RefreshIdentityAsync();
            return user.MapTo<User, UserDto>();
        }

        /// <summary>
        /// 刷新Token
        /// </summary> 
        public async Task RefreshTokenAsync(string token)
        {
            var log = await loginLogs.GetAsync(token);
            if (log != null)
            {
                log.ExpiredTime = DateTime.Now.AddDays(1);
                log.LastTime = DateTime.Now;
                await loginLogs.AddAsync(log);
                await loginLogs.CommmitAsync();
            }
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> ExistsTokenAsync(string token)
        {
            var log = await loginLogs.GetAsync(token); 
            return log != null && log.ExpiredTime > DateTime.Now;
        }
    }
}
