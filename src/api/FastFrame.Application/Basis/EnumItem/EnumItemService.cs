using FastFrame.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class EnumItemService
    {
        public async Task<IEnumerable<EnumItemModel>> GetValues(EnumName name)
        {
            return await enumItemRepository
                .Where(v => v.Key == name)
                .OrderBy(v => v.Order)
                .ThenBy(v => v.Value)
                .Select(v => new EnumItemModel
                {
                    Id = v.Id,
                    Key = v.Key,
                    Value = v.Value,
                    Super_Id = v.Super_Id
                })

                .ToListAsync();
        }

        public async Task<IEnumerable<EnumItemDto>> GetChildren(EnumName name)
        {
            return await Query()
                .Where(v => v.Key == name && v.Super_Id == null)
                .OrderBy(v => v.Order)
                .ThenBy(v => v.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<EnumItemDto>> GetChildren(string id)
        {
            return await Query()
                .Where(v => v.Super_Id == id)
                .OrderBy(v => v.Order)
                .ThenBy(v => v.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<EnumName>> GetHasChildrenNames()
        {
            return await enumItemRepository
                .Select(v => v.Key)
                .Distinct()
                .ToListAsync();
        }
    }
}
