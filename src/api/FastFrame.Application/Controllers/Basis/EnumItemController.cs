using FastFrame.Dto.Basis;
using FastFrame.Entity.Enums;
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
    }
}
