using FastFrame.Dto.Basis;
using FastFrame.Dto.Dtos;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository.Basis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Service.Services
{
    public class AccountService : IService
    {
        private readonly UserRepository userRepository;
        private readonly ICurrentUserProvider currentUserProvider;

        public AccountService(UserRepository userRepository, ICurrentUserProvider currentUserProvider)
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
        /// 更新用户信息
        /// </summary>
        /// <param name="input"></param>
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
                input.HandIconId,
            }.MapSet(user);
            await userRepository.UpdateAsync(user);
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
