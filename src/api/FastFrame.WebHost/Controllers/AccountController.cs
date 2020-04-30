﻿using System.Collections;
using System.Threading.Tasks;
using FastFrame.Dto.Basis;
using FastFrame.Dto.Dtos;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using FastFrame.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFrame.WebHost.Controllers
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
        [EveryoneAccess]
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
        [EveryoneAccess]
        public async Task<UserDto> Regist([FromBody]UserDto user)
        {
            return await service.RegistAsync(user);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [EveryoneAccess]
        public async Task LogOut()
        {
            await AppSession.LogOutAsync();
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

 
