using FastFrame.Dto.Basis;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class MeidiaController
    {
        [Permission(new string[] { nameof(List) })]
        [HttpGet("{id}")]
        public async Task<MeidiaOutput> Meidias(string id, string v = "")
        {
            if (id == "null")
                id = null;
            return await service.Meidias(id, v);
        }
    }
}
