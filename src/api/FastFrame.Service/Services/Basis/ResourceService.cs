using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class ResourceService
    {
        public Task<string> GetPathByMd5Async(string md5)
            => resourceRepository
                        .Where(r => r.MD5 == md5)
                        .Select(r => r.Path)
                        .FirstOrDefaultAsync();

        protected override async Task OnAdding(ResourceDto input, Resource entity)
        {
            entity.Uploader_Id = AppSession?.CurrUser?.Id;
            entity.UploadTime = DateTime.Now;

            await base.OnAdding(input, entity);
        }
    }
}
