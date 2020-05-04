using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
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
        public Task<PageList<UserViewModel>> UserList(string qs)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(qs.ToObject<Pagination>()); 
    }
}
