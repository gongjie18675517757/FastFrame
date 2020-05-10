using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;

namespace FastFrame.WebHost.Controllers.OA
{
    public partial class OaLeaveController
    {
        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<PageList<UserViewModel>> UserList(string qs)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(qs.ToObject<Pagination>());

        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<PageList<DeptViewModel>> DeptList(string qs)
            => Request.HttpContext.RequestServices
                    .GetService<DeptService>().ViewModelListAsync(qs.ToObject<Pagination>());
    }
}
