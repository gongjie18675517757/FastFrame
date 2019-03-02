using FastFrame.Dto.Basis;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class MeidiaController
    {
        [Permission(new string[] { nameof(List) })]
        [HttpGet("{id}")]
        public async Task<IEnumerable<MeidiaDto>> Meidias(string id)
        {
            if (id == "null")
                id = null;
            return await service.Meidias(id);
        }
    }
}
