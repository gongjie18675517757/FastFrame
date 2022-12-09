	using FastFrame.Application.OA; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
namespace FastFrame.WebHost.Controllers.OA
{
		
	/// <summary>
	/// 请假单 
	/// </summary>
	public partial class OaLeaveController:BaseCURDController<OaLeaveDto>
	{
		private readonly OaLeaveService service;
		
		public OaLeaveController(OaLeaveService service)
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
