using FastFrame.Dto.Basis;
using FastFrame.Infrastructure;
using FastFrame.Application.Services.Basis;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using FastFrame.Infrastructure.Attrs;

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
