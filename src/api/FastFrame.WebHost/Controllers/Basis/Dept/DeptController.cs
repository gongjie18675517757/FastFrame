using FastFrame.Application;
using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
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

        
        [HttpGet("{id?}")]
        public async Task<IEnumerable<ITreeModel>> GetChildrenBySuperId(string id)
        {
            return await service.GetChildrenBySuperId(id);
        }
    }
}
