using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Services.Basis
{
    public partial class TenantService
    {
        private string Tenant_Id => AppSession?.Tenant_Id;
        public async Task<TenantDto> GetCurrentAsync()
        {
            var id = await tenantRepository.Where(v => v.Tenant_Id == Tenant_Id).Select(v => v.Id).FirstOrDefaultAsync();
            return await GetAsync(id);
        }

        public async Task UpdateCurrentAsync(TenantDto tenantDto)
        {
            var id = await tenantRepository.Where(v => v.Tenant_Id == Tenant_Id).Select(v => v.Id).FirstOrDefaultAsync();
            tenantDto.Id = id;
            await UpdateAsync(tenantDto);
        }

        protected override async Task OnAdding(TenantDto input, Tenant entity)
        {
            await base.OnAdding(input, entity);
            entity.Super_Id = Tenant_Id;
            entity.Tenant_Id = IdGenerate.NetId();
        }

        protected override IQueryable<TenantDto> GetListQueryableing(IQueryable<TenantDto> query)
        {
            query = base.GetListQueryableing(query);
            var isRoot = Tenant_Id.IsNullOrWhiteSpace();
            query = query.Where(x => x.Super_Id == Tenant_Id || isRoot);
            return query;
        }
    }
}
