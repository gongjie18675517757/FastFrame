using FastFrame.Application;
using FastFrame.Application.Basis;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
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
        public Task<IEnumerable<IViewModel>> UserList(string kw, int page_index = 1, int page_size = 10)
            => service.ViewModelListAsync<User>(kw, page_index, page_size);


        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<IEnumerable<IViewModel>> DeptList(string kw, int page_index = 1, int page_size = 10)
            => service.ViewModelListAsync<Dept>(kw, page_index, page_size);


        [HttpGet("{id?}")]
        public async Task<IEnumerable<ITreeModel>> GetChildrenBySuperId(string id)
        {
            return await service.GetChildrenBySuperId(id);
        }
    }
}
