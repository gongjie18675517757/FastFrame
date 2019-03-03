using FastFrame.Dto.Basis;
using FastFrame.Dto.Dtos;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services
{
    public class AccountService : IService
    {
        private readonly IRepository<User> userRepository;
        private readonly ICurrentUserProvider currentUserProvider;

        public AccountService(IRepository<User> userRepository, ICurrentUserProvider currentUserProvider)
        {
            this.userRepository = userRepository;
            this.currentUserProvider = currentUserProvider;
        }
        public Task<bool> VerifyUnique(string id, string propName, string value)
        {
            throw new NotImplementedException();
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
                if (user.IsDisabled)
                    throw new Exception("帐号已被停用");
                var curr = new CurrUser()
                {
                    IsAdmin = user.IsAdmin,
                    IsRoot = user.IsRoot,
                    ToKen = IdGenerate.NetId(),
                    Id = user.Id,
                    Name = user.Name,
                    Account = user.Account
                };
                await currentUserProvider.Login(curr);
                return curr;
            }
            throw new Exception("登陆失败,帐号或者密码错误!");
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
            var userId = currentUserProvider.GetCurrUser().Id;
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
            var curr = currentUserProvider.GetCurrUser();
            if (curr == null)
                throw new Exception("未登陆吧!");
            var user = await userRepository.GetAsync(curr?.Id);
            return user.MapTo<User, UserDto>();
        }
    }
}
