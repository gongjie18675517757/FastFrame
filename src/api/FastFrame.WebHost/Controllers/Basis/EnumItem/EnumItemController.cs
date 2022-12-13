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
        public async Task<IEnumerable<EnumItemModel>> GetValues(int? name)
        {
            return await service.GetValues(name);
        }  

        [Permission(new string[] { "Add", "Update" })]
        [HttpGet("{name?}")]
        public Task<IEnumerable<IViewModel>> EnumItemList(int? name, string kw, int page_index = 1, int page_size = 10)
        {
            return service.EnumItemList(name, kw, page_index, page_size);
        }
    }
}
