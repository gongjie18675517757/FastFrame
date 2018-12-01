using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class TenantService
    {
        private readonly ICurrentUserProvider currentUserProvider;

        public TenantService(
            ICurrentUserProvider currentUserProvider,
            IRepository<Foreign> foreignRepository,
            IRepository<User> userRepository,
            IRepository<Tenant> tenantRepository, 
            IScopeServiceLoader loader)
            :this(foreignRepository, userRepository, tenantRepository, loader)
        {
            this.currentUserProvider = currentUserProvider;
        }
        public Task<TenantDto> GetCurrentAsync()
        {
            return GetAsync(currentUserProvider.GetCurrOrganizeId());
        } 
        public Task<TenantDto> UpdateCurrentAsync(TenantDto tenantDto)
        {
            tenantDto.Id = currentUserProvider.GetCurrOrganizeId();
            return UpdateAsync(tenantDto);
        }
    }
}
