using FastFrame.Application.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Account
{
    public class AccountService : IService
    {
        private readonly IRepository<User> userRepository;
        private readonly IIdentityManager identityManager;
        private readonly IAppSessionProvider appSession;

        public AccountService(IRepository<User> userRepository, IIdentityManager identityManager, IAppSessionProvider appSession)
        {
            this.userRepository = userRepository;
            this.identityManager = identityManager;
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
                if (user.Enable == EnabledMark.disabled)
                    throw new MsgException("帐号已被停用");

                var identity = await identityManager.GenerateIdentity(user.Id);

                var curr = new CurrUser()
                {
                    IsAdmin = user.IsAdmin,
                    ToKen = identity.GetToken(),
                    Id = user.Id,
                    Name = user.Name,
                    Account = user.Account
                };

                await userRepository.CommmitAsync();
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

                await identityManager.SetUserAllTokenFailureAsync(user.Id);
            }
            await userRepository.UpdateAsync(user);
            await userRepository.CommmitAsync();
             
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
    }
}
