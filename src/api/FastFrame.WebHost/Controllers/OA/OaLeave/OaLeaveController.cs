using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using FastFrame.Application;

namespace FastFrame.WebHost.Controllers.OA
{
    public partial class OaLeaveController
    {
        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<IPageList<UserViewModel>> UserList(string qs)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(Pagination<UserViewModel>.FromJson(qs));

        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<IPageList<DeptViewModel>> DeptList(string qs)
            => Request.HttpContext.RequestServices
                    .GetService<DeptService>().ViewModelListAsync(Pagination<DeptViewModel>.FromJson(qs));
    }
}
