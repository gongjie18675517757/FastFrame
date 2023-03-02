using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Cache;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Resource;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class ResourceService : IService, IResourceStoreProvider
    {
        private readonly IRepository<Resource> resourceRepository;
        private readonly ICacheProvider cacheProvider;
        private readonly IResourceProvider resourceProvider;
        private readonly IApplicationSession sessionProvider;

        public ResourceService(IRepository<Resource> resourceRepository,
                               ICacheProvider cacheProvider,
                               IResourceProvider resourceProvider,
                               IApplicationSession sessionProvider)
        {
            this.resourceRepository = resourceRepository;
            this.cacheProvider = cacheProvider;
            this.resourceProvider = resourceProvider;
            this.sessionProvider = sessionProvider;
        }


        public async Task<string> GetPathByMd5Async(string md5)
        {
            if (string.IsNullOrWhiteSpace(md5))
                return null;

            return await resourceRepository
                        .Where(r => r.MD5 == md5)
                        .Select(r => r.Path)
                        .FirstOrDefaultAsync();
        }

        [AutoCacheInterceptor]
        public virtual async Task<IResourceRedearInfo> TryGetResourceReader(string resourceId)
        {
            var info = await cacheProvider.GetAsync<Resource>(resourceId);

            info ??= await resourceRepository
                          .Where(v => v.Id == resourceId)
                          .MapTo<Resource, Resource>()
                          .FirstOrDefaultAsync();

            if (info == null)
                return null;

            await cacheProvider.SetAsync(resourceId, info, TimeSpan.FromDays(1));

            var resourceReader = await resourceProvider.ReadAsync(info.Path);

            return new LocalFileResourceStreamModel(info.Name, info.ContentType, info.UploadTime, resourceReader);
        }

        public async Task<IResourceInfo> TrySaveResource(string name, string contentType, Stream stream)
        {
            var md5 = stream.ToMD5();
            var path = await GetPathByMd5Async(md5);

            if (path.IsNullOrWhiteSpace())
                path = await resourceProvider.WriteAsync(stream, name, contentType);


            return await SaveToDatabase(name, contentType, stream.Length, md5, path);
        }

        public async Task<IResourceInfo> TrySaveEmptyResource(string name, string contentType, long size, string md5)
        {
            var path = await resourceProvider.WriteAsync(new MemoryStream(), name, contentType);
            return await SaveToDatabase(name, contentType, size, md5, path);
        }

        private static Regex replace_name_regex = file_name_replace_regex();

        private async Task<IResourceInfo> SaveToDatabase(string name, string contentType, long size, string md5, string path)
        {
            if (!name.IsNullOrWhiteSpace())
                name = replace_name_regex.Replace(name, "_");

            var curr = sessionProvider.CurrUser;
            var resource = await resourceRepository.AddAsync(new Resource
            {
                ContentType = contentType,
                Name = name,
                Path = path,
                Size = size,
                MD5 = md5,
                UploadTime = DateTime.Now,
                Uploader_Id = curr?.Id,
                Id = null
            });

            await resourceRepository.CommmitAsync();

            var model = resource.MapTo<Resource, ResourceModel>();

            if (curr != null)
                model.UploaderName = curr.Name;

            return model;
        }

        [GeneratedRegex("([^\\u4e00-\\u9fa50-9a-zA-Z\\.])+", RegexOptions.Compiled)]
        private static partial Regex file_name_replace_regex();
    }
}
