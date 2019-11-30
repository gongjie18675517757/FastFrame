using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class MeidiaService
    {
        public async Task<MeidiaOutput> Meidias(string id = null, string keyword = "")
        {
            var output = new MeidiaOutput();
            if (string.IsNullOrWhiteSpace(id) || !string.IsNullOrWhiteSpace(keyword))
                output.Curr = null;
            else
                output.Curr = await meidiaRepository.GetAsync(id);


            var query = QueryMain();
            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(r => r.Name.Contains(keyword));
            else
                query = query.Where(r => r.Parent_Id == id);
            output.Children = await query.OrderByDescending(x => x.IsFolder)
                .ThenBy(x => x.CreateTime)
                .ToListAsync();

            //  new MeidiaOutput
            //{
            //    Curr = await meidiaRepository.GetAsync(id),
            //    Children = await QueryMain()
            //    .Where(x => x.Parent_Id == id)
            //    .OrderByDescending(x => x.IsFolder)
            //    .ThenBy(x => x.CreateTime)
            //    .ToListAsync()
            //};

            return output;
        }
    }
}
