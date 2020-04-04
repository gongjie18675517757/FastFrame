using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class TenantService
    {
        private readonly ICurrentUserProvider currentUserProvider;

        public TenantService(
            ICurrentUserProvider currentUserProvider, 
            IRepository<User> userRepository,
            IRepository<Tenant> tenantRepository,
            IScopeServiceLoader loader)
            : this(userRepository, tenantRepository, loader)
        {
            this.currentUserProvider = currentUserProvider;
        }
        public Task<TenantDto> GetCurrentAsync()
        {
            return GetAsync(currentUserProvider.GetCurrOrganizeId().Id);
        }
        public Task UpdateCurrentAsync(TenantDto tenantDto)
        {
            tenantDto.Id = currentUserProvider.GetCurrOrganizeId().Id;
            return UpdateAsync(tenantDto);
        }

        protected override IQueryable<TenantDto> GetListQueryableing(IQueryable<TenantDto> query)
        {
            var terantId = currentUserProvider.GetCurrOrganizeId().Id;
            query = base.GetListQueryableing(query);
            query.Where(x => x.Id != terantId && x.Super_Id == terantId);
            return query;
        }
    }
}
