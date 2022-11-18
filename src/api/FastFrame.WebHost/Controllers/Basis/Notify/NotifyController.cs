﻿using FastFrame.Application;
using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class NotifyController
    {
        /// <summary>
        /// 所有通知
        /// </summary> 
        [HttpGet]
        public async Task<IPageList<NotifyDto>> AllList(string qs)
        {
            return await service.PageListAsync(Pagination<NotifyDto>.FromJson(qs));
        }

        /// <summary>
        /// 通知内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<NotifyDto> GetNotify(string id)
        {
            return await service.GetAsync(id);
        }


        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<IPageList<UserViewModel>> UserList(string qs)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(Pagination<UserViewModel>.FromJson(qs));
    }
}
