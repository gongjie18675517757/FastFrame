	using FastFrame.Application.Basis; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
	/// <summary>
	/// 多租户信息 
	/// </summary>
	public partial class TenantController:BaseController<TenantDto>
	{
		private readonly TenantService service;
		
		public TenantController(TenantService service)
			 : base(service)
		{
			this.service = service;
		}
		
		[HttpGet("{super_id?}")]
		public IAsyncEnumerable<ITreeModel> TreeList(string super_id) 
		{
			return service.TreeModelListAsync(super_id);
		}
		
	}
}
