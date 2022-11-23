using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class MeidiaService
    {
        public async Task<MeidiaOutput> Meidias(string id = null, string keyword = "")
        {

            IQueryable<MeidiaModel> query = MeidiaQuery();

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(r => r.Name.Contains(keyword));
            else
                query = query.Where(r => r.Super_Id == id);

            return new MeidiaOutput
            {
                Children = await query.ToListAsync(),
                Super_Id = keyword.IsNullOrWhiteSpace() ?
                                    await meidiaRepository
                                            .Where(v => v.Id == id)
                                            .Select(v => v.Super_Id)
                                            .FirstOrDefaultAsync()
                                            : null
            };
        }

        private IQueryable<MeidiaModel> MeidiaQuery()
        {
            var resourceRepository = loader.GetService<IRepository<Resource>>();
            return from a in meidiaRepository
                   join b in resourceRepository on a.Resource_Id equals b.Id into t_b
                   from b in t_b.DefaultIfEmpty()
                   join c in userRepository on a.Create_User_Id equals c.Id
                   orderby a.IsFolder descending, a.Id descending
                   select new MeidiaModel
                   {
                       Id = a.Id,
                       ContentType = b.ContentType,
                       Resource_Id = a.Resource_Id,
                       CreateName = c.Name,
                       Name = a.Name,
                       CreateTime = a.CreateTime,
                       IsFolder = a.IsFolder,
                       Size = b.Size,
                       Super_Id = a.Super_Id
                   };
        }

        public async Task ReName(string id, string name)
        {
            var entity = await meidiaRepository.GetAsync(id);
            if (entity != null)
            {
                entity.Name = name;
                await meidiaRepository.UpdateAsync(entity);
                await meidiaRepository.CommmitAsync();
            }
        }

        protected override async Task OnDeleteing(Meidia input)
        {
            await base.OnDeleteing(input);
            if (input.IsFolder)
            {
                var children = await meidiaRepository.Where(v => v.Super_Id == input.Id).ToListAsync();
                foreach (var item in children)
                {
                    item.Super_Id = input.Super_Id;
                    await meidiaRepository.UpdateAsync(item);
                }
            }
        }

        public Task<MeidiaModel> GetMeidia(string id)
        {
            return MeidiaQuery().Where(v => v.Id == id).FirstOrDefaultAsync();
        }
    }
}
