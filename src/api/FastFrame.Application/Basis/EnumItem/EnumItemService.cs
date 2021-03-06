﻿using FastFrame.Entity.Enums;
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
                .Select(v => new EnumItemModel
                {
                    Id = v.Id,
                    Key = v.Key,
                    Value = v.Value,
                    Super_Id = v.Super_Id
                })
                .ToListAsync();
        }
    }
}
