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
	///组织信息 服务实现 
	/// </summary>
	public partial class TenantService:BaseService<Tenant, TenantDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Tenant> tenantRepository;
		
		public TenantService(IRepository<User> userRepository,IRepository<Tenant> tenantRepository,IScopeServiceLoader loader)
			:base(tenantRepository,loader)
		{
			this.userRepository=userRepository;
			this.tenantRepository=tenantRepository;
		}
		
		
		protected override IQueryable<TenantDto> QueryMain() 
		{
			 var query = from _tenant in tenantRepository 
						 select new TenantDto
						{
							FullName=_tenant.FullName,
							ShortName=_tenant.ShortName,
							UrlMark=_tenant.UrlMark,
							Parent_Id=_tenant.Parent_Id,
							CanHaveChildren=_tenant.CanHaveChildren,
							HandIcon_Id=_tenant.HandIcon_Id,
							Id=_tenant.Id,
					};
			return query;
		}
		
	}
}
