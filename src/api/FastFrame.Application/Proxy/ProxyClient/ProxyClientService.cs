using FastFrame.Entity.Proxy;
using FastFrame.Infrastructure;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Buffers;

namespace FastFrame.Application.Proxy
{
    public partial class ProxyClientService
    {
        protected override async Task OnAddOrUpdateing(ProxyClientDto input, ProxyClient entity)
        {
            await base.OnAddOrUpdateing(input, entity);

            await Loader
                .GetService<HandleOne2ManyService<ProxyTarget, ProxyTarget>>()
                .UpdateManyAsync(
                    v => v.ProxyClient_Id == entity.Id,
                    input.ProxyDic.Values.Where(v => v != null),
                    (a, b) => a.Id == b.Id,
                    v =>
                    {
                        v.ProxyClient_Id = entity.Id;
                        return v;
                    },
                    (before, after) =>
                    {
                        after.MapSet(before);
                    }
                );
        }

        protected override async Task OnDeleteing(ProxyClient entity)
        {
            await base.OnDeleteing(entity);

            

            await Loader
                .GetService<HandleOne2ManyService<ProxyTarget, ProxyTarget>>()
                .DelManyAsync(v => v.ProxyClient_Id == entity.Id);
        }

        protected override async Task OnGeting(ProxyClientDto output)
        {
            await base.OnGeting(output);

            var proxy_list = await Loader.GetService<IRepository<ProxyTarget>>().Where(v => v.ProxyClient_Id == output.Id).ToListAsync();

            output.ProxyDic = Enum
                .GetValues<ProxyTargetEnum>()
                .ToDictionary(v => v, v => proxy_list.FirstOrDefault(x => x.TargetEnum == v));
        }
    }
}
