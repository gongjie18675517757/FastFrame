	using FastFrame.Application.Basis; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
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
