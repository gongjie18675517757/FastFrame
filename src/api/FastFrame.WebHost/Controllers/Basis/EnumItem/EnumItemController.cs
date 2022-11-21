using FastFrame.Application;
using FastFrame.Application.Basis;
using FastFrame.Entity;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class EnumItemController
    {
        [HttpGet("{name}")]
        public async Task<IEnumerable<EnumItemModel>> GetValues(EnumName name)
        {
            return await service.GetValues(name);
        }

        [Permission("Get", "查看")]
        [HttpGet("{name}")]
        public async Task<IEnumerable<ITreeModel>> GetChildrenByName(EnumName name)
        {
            return await service.GetChildren(name);
        }

        [Permission(new string[] { "List" })]
        [HttpGet("{id}")]
        public async Task<IEnumerable<ITreeModel>> GetChildrenBySuperId(string id)
        {
            return await service.GetChildren(id);
        }

        [Permission("Get", "查看")]
        [HttpGet]
        public async Task<IEnumerable<EnumName?>> GetHasChildrenNames()
        {
            return await service.GetHasChildrenNames();
        }

        [Permission(new string[] { "Add", "Update" })]
        [HttpGet("{name?}")]
        public Task<IEnumerable<IViewModel>> EnumItemList(EnumName? name, string kw, int page_index = 1, int page_size = 10)
        {
            return service.EnumItemList(name, kw, page_index, page_size);
        }
    }
}
