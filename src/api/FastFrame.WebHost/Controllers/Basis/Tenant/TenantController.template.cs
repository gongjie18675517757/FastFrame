namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Application.Basis; 
	using FastFrame.Infrastructure.Permission; 
	using FastFrame.Infrastructure.Interface; 
		
	/// <summary>
	/// 组织信息 
	/// </summary>
	public partial class TenantController:BaseController<TenantDto>
	{
		private readonly TenantService service;
		
		public TenantController(TenantService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
