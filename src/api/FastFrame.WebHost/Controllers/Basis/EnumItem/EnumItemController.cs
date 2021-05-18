using FastFrame.Application.Basis;
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

        [HttpGet]
        [Permission(new string[] { "Add", "Update" })]
        public Task<PageList<EnumItemViewModel>> EnumItemList(string qs)
            => service.ViewModelListAsync(qs.ToObject<Pagination>(true));
    }
}
