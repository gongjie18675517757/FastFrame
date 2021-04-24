﻿using FastFrame.Application.Account;
using FastFrame.Application.Basis;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Account
{
    /// <summary>
    /// 登陆
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly AccountService service;
        private readonly IAppSessionProvider appSession;

        public AccountController(AccountService service, IAppSessionProvider appSession)
        {
            this.service = service;
            this.appSession = appSession;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost] 
        [AllowAnonymous]
        public async Task<CurrUser> Login([FromBody]LoginInput input)
        { 
            return await service.LoginAsync(input);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<UserDto> Regist([FromBody]UserDto user)
        {
            return await service.RegistAsync(user);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task LogOut()
        {
            await appSession.LogOutAsync();
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDto> GetCurrent()
        {
            return await service.GetCurrentAsync();
        }

        /// <summary>
        /// 修改当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<UserDto> UpdateCurrUserInfo([FromBody]UserDto input)
        {
            return await service.UpdateUserInfo(input);
        }
    } 
    
}

 
