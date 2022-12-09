using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using FastFrame.Application;
using FastFrame.Entity;
using FastFrame.Entity.Basis;

namespace FastFrame.WebHost.Controllers.OA
{
    public partial class OaLeaveController
    {
        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public IAsyncEnumerable<IViewModel> UserList(string kw, int page_index = 1, int page_size = 10)
            => service.loader.GetService<UserService>().ViewModelListAsync(kw, page_index, page_size);


        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public IAsyncEnumerable<IViewModel> DeptList(string kw, int page_index = 1, int page_size = 10)
            => service.loader.GetService<DeptService>().ViewModelListAsync(kw, page_index, page_size);
    }
}
