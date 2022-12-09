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
        public IAsyncEnumerable<IViewModel> UserList(string kw, int page_index = 1, int page_size = 10)
            => service.loader.GetService<UserService>().ViewModelListAsync(kw, page_index, page_size);


        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public IAsyncEnumerable<IViewModel> DeptList(string kw, int page_index = 1, int page_size = 10)
            => service.loader.GetService<DeptService>().ViewModelListAsync(kw, page_index, page_size); 
         
        //[HttpGet("{id?}")]
        //public IAsyncEnumerable<ITreeModel> TreeList(string id)
        //{
        //    return service.TreeModelListAsync(id);
        //}
    }
}
