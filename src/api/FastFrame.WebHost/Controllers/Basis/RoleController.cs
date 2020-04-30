using FastFrame.Dto.Basis;
using FastFrame.Infrastructure;
using FastFrame.Application.Services.Basis;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using FastFrame.Infrastructure.Attrs;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class RoleController
    {
        [Permission(new string[] { "Add", "Update" })]
        [HttpPost]
        public Task<PageList<UserViewModel>> UserList(Pagination Pagination)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(Pagination);

        [HttpGet]
        [Permission(new string[] { "Add" })]
        public Task<RolePermissionModel[]> PermissionList()
                => Request.HttpContext.RequestServices
                    .GetService<RolePermissionService>().GetPermissionModelListAsync();

    }
}
