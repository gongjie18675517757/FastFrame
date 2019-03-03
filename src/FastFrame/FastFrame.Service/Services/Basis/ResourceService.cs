using FastFrame.Dto.Basis;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class ResourceService
    {
        public override async Task<ResourceDto> AddAsync(ResourceDto input)
        {
            var md5 = input.MD5;
            var id = await resourceRepository.Where(r => r.MD5 == md5).Select(r => r.Id).FirstOrDefaultAsync();
            if (id == null)
                return await base.AddAsync(input);
            else
                return await GetAsync(id);
        }
    }
}
