using FastFrame.Dto.Basis;
using FastFrame.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class EnumItemService
    {
        public async Task<IEnumerable<EnumItemDto>> GetValues(EnumName name)
        {
            return await Query().Where(v => v.Key == name).ToListAsync();
        }
    }
}
