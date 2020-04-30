namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Application.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
		
	/// <summary>
	/// 组织信息 
	/// </summary>
	[Permission(nameof(Tenant),"组织信息")]
	public partial class TenantController:BaseController<Tenant, TenantDto>
	{
		private readonly TenantService service;
		
		public TenantController(TenantService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
