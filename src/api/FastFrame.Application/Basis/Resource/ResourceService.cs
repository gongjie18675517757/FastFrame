﻿using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Resource;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class ResourceService : IService, IResourceStoreProvider
    {
        private readonly IRepository<Resource> resourceRepository;
        private readonly IResourceProvider resourceProvider;
        private readonly IApplicationSession sessionProvider;

        public ResourceService(IRepository<Resource> resourceRepository,
                               IResourceProvider resourceProvider,
                               IApplicationSession sessionProvider)
        {
            this.resourceRepository = resourceRepository;
            this.resourceProvider = resourceProvider;
            this.sessionProvider = sessionProvider;
        }
        public Task<string> GetPathByMd5Async(string md5)
            => resourceRepository
                        .Where(r => r.MD5 == md5)
                        .Select(r => r.Path)
                        .FirstOrDefaultAsync();


        public async Task<IResourceRedearInfo> TryGetResourceReader(string resourceId)
        {
            var info = await resourceRepository
                .Where(v => v.Id == resourceId)
                .Select(v => new { v.Name, v.ContentType, v.Path, v.UploadTime })
                .FirstOrDefaultAsync();

            if (info == null)
                return null;



            var resourceReader = await resourceProvider.ReadAsync(info.Path);

            return new LocalFileResourceStreamModel(info.Name, info.ContentType, info.UploadTime, resourceReader);
        } 
    
        public async Task<IResourceInfo> TrySaveResource(string name, string contentType, Stream stream)
        {
            var md5 = stream.ToMD5();
            var path = await GetPathByMd5Async(md5);

            if (path.IsNullOrWhiteSpace())
                path = await resourceProvider.WriteAsync(stream, name);

            var curr = sessionProvider.CurrUser;
            var resource = await resourceRepository.AddAsync(new Resource
            {
                ContentType = contentType,
                Name = name,
                Path = path,
                Size = stream.Length,
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
    }
}
