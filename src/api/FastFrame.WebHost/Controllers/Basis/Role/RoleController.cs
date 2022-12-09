using FastFrame.Application;
using FastFrame.Application.Basis;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class RoleController
    {
        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public IAsyncEnumerable<IViewModel> UserList(string kw, int page_index = 1, int page_size = 10)
            => service.loader.GetService<UserService>().ViewModelListAsync(kw, page_index, page_size);

        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public IAsyncEnumerable<IViewModel> RoleList(string kw, int page_index = 1, int page_size = 10)
            => service.loader.GetService<RoleService>().ViewModelListAsync(kw, page_index, page_size);
    }
}
