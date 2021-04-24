using FastFrame.Application.Events;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class ResourceMapService : IService,
        IEventHandle<DoMainAdding<IHaveMultiFileDto>>,
        IEventHandle<DoMainDeleteing<IHaveMultiFileDto>>,
        IEventHandle<DoMainUpdateing<IHaveMultiFileDto>>,
        IRequestHandle<IEnumerable<ResourceModel>, IHaveMultiFileDto>,
        IRequestHandle<IEnumerable<KeyValuePair<string, IEnumerable<ResourceModel>>>, IEnumerable<IHaveMultiFileDto>>
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Resource> resources;
        private readonly IRepository<ResourceMap> maps;
        private readonly HandleOne2ManyService<ResourceModel, ResourceMap> manyService;

        public ResourceMapService(IRepository<User> users, IRepository<Resource> resources, IRepository<ResourceMap> maps, HandleOne2ManyService<ResourceModel, ResourceMap> manyService)
        {
            this.users = users;
            this.resources = resources;
            this.maps = maps;
            this.manyService = manyService;
        }
        private async Task HandleItems(string id, IEnumerable<ResourceModel> items)
        {
            await manyService.UpdateManyAsync(
                        v => v.FKey_Id == id,
                        items,
                        (a, b) => a.File_Id == b.ContentType && a.Key == b.Key,
                        v => new ResourceMap
                        {
                            Id = null,
                            Key = v.Key,
                            File_Id = v.Id,
                            FKey_Id = id
                        });
        }

        public Task HandleEventAsync(DoMainAdding<IHaveMultiFileDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Files);
        }

        public Task HandleEventAsync(DoMainDeleteing<IHaveMultiFileDto> @event)
        {
            return HandleItems(@event.Id, null);
        }

        public Task HandleEventAsync(DoMainUpdateing<IHaveMultiFileDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Files);
        }

        private IQueryable<ResourceModel> Query()
        {
            return from a in maps
                   join b in resources on a.File_Id equals b.Id
                   join c in users on b.Uploader_Id equals c.Id into t_c
                   from c in t_c.DefaultIfEmpty()
                   select new ResourceModel
                   {
                       Id = b.Id,
                       ContentType = b.ContentType,
                       Key = a.Key,
                       Name = b.Name,
                       Size = b.Size,
                       UploaderName = c.Name,
                       UploadTime = b.UploadTime,
                       FKey_Id = a.FKey_Id
                   };
        }

        public async Task<IEnumerable<ResourceModel>> HandleRequestAsync(IHaveMultiFileDto request)
        {
            return await Query().Where(v => v.FKey_Id == request.Id).ToListAsync();
        }

        public async Task<IEnumerable<KeyValuePair<string, IEnumerable<ResourceModel>>>> HandleRequestAsync(IEnumerable<IHaveMultiFileDto> request)
        {
            var keys = request.Select(v => v.Id).ToList();
            var list = await Query().Where(v => keys.Contains(v.FKey_Id)).ToListAsync();
            return list.GroupBy(v => v.FKey_Id).Select(v => new KeyValuePair<string, IEnumerable<ResourceModel>>(v.Key, v));
        }
    }
}
