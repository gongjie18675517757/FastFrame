﻿using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class DeptController
    {
        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<PageList<UserViewModel>> UserList(string qs)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(qs.ToObject<Pagination>(true));

        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<PageList<DeptViewModel>> DeptList(string qs)
            => service.ViewModelListAsync(qs.ToObject<Pagination>(true));
    }
}
