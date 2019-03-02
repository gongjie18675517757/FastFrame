namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///组织信息 
	/// </summary>
	[Permission(nameof(Tenant),"组织信息")]
	public partial class TenantController:BaseController<Tenant, TenantDto>
	{
		private readonly TenantService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		public TenantController(TenantService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		
		
	}
}
