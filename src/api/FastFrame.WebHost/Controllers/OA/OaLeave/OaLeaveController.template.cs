	using FastFrame.Application.OA; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
namespace FastFrame.WebHost.Controllers.OA
{
		
	/// <summary>
	/// 请假单 
	/// </summary>
	public partial class OaLeaveController(OaLeaveService service):BaseCURDController<OaLeaveDto>(service)
	{
		private readonly OaLeaveService service=service;
		
		
		[HttpGet()]
		public IAsyncEnumerable<ITreeModel> TreeList(string super_id,string kw) 
		{
			return service.TreeListAsync(super_id,kw);
		}
		
	}
}
