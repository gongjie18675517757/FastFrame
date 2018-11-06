using System.Collections;
using System.Threading.Tasks;
using FastFrame.Dto.Basis;
using FastFrame.Dto.Dtos;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFrame.Application.Controllers
{
    /// <summary>
    /// 登陆
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly AccountService service;

        public AccountController(AccountService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async ValueTask<CurrUser> Login([FromBody]LoginInput input)
        {
            return await service.LoginAsync(input);
        }


        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<UserDto> GetCurrent()
        {
            return await service.GetCurrentAsync();
        }

        /// <summary>
        /// 修改当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async ValueTask<UserDto> UpdateCurrUserInfo(UserDto input)
        {
            return await service.UpdateUserInfo(input);
        }
    } 
    
}

 
