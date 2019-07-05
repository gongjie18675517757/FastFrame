namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	/// <summary>
	/// 服务类 
	/// </summary>
	public partial class TenantHostService:BaseService<TenantHost, TenantHostDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<TenantHost> tenantHostRepository;
		
		public TenantHostService(IRepository<User> userRepository,IRepository<TenantHost> tenantHostRepository,IScopeServiceLoader loader)
			:base(tenantHostRepository,loader)
		{
			this.userRepository=userRepository;
			this.tenantHostRepository=tenantHostRepository;
		}
		
		
		protected override IQueryable<TenantHostDto> QueryMain() 
		{
			var tenantHostQueryable=tenantHostRepository.Queryable;
			 var query = from _tenantHost in tenantHostQueryable 
						 select new TenantHostDto
						{
							Host=_tenantHost.Host,
							Id=_tenantHost.Id,
					};
			return query;
		}
		
	}
}
