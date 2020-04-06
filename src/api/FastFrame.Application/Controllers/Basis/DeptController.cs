using FastFrame.Dto.Basis;
using FastFrame.Infrastructure;
using FastFrame.Service.Services.Basis;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using FastFrame.Infrastructure.Attrs;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class DeptController
    {
        [Permission(new string[] { "Add", "Update" })]
        [HttpPost]
        public Task<PageList<UserViewModel>> UserList(PagePara pagePara)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(pagePara);

        [Permission(new string[] { "Add", "Update" })]
        [HttpPost]
        public Task<PageList<DeptViewModel>> DeptList(PagePara pagePara)
            => service.ViewModelListAsync(pagePara);
    }
}
