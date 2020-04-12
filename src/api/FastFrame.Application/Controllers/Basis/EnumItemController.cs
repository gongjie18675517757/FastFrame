using FastFrame.Dto.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class EnumItemController
    {
        [HttpGet("{name}")]
        public async Task<IEnumerable<EnumItemDto>> GetValues(EnumName name)
        {
            return await service.GetValues(name);
        }

        [HttpPost()]
        [Permission(new string[] { "Add", "Update" })]
        public Task<PageList<EnumItemViewModel>> EnumItemList(Pagination pagination)
            => service.ViewModelListAsync(pagination);
    }
}
