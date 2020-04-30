﻿using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class DeptController
    {
        [Permission(new string[] { "Add", "Update" })]
        [HttpPost]
        public Task<PageList<UserViewModel>> UserList(Pagination Pagination)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(Pagination);

        [Permission(new string[] { "Add", "Update" })]
        [HttpPost]
        public Task<PageList<DeptViewModel>> DeptList(Pagination Pagination)
            => service.ViewModelListAsync(Pagination);
    }
}
