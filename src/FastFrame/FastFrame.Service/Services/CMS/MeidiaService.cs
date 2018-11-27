using FastFrame.Dto.CMS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.CMS
{
    public partial class MeidiaService
    {
        public async Task<IEnumerable<MeidiaDto>> Meidias(string id = null)
        {
            return await QueryMain()
                .Where(x => x.Parent_Id == id)
                .OrderByDescending(x => x.IsFolder)
                .ThenBy(x => x.CreateTime)
                .ToListAsync();
        } 
    }
}
