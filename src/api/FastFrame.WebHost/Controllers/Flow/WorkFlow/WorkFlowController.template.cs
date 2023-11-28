	using FastFrame.Application.Flow; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
namespace FastFrame.WebHost.Controllers.Flow
{
		
	/// <summary>
	/// 工作流 
	/// </summary>
	public partial class WorkFlowController(WorkFlowService service):BaseCURDController<WorkFlowDto>(service)
	{
		private readonly WorkFlowService service=service;
		
		
		[HttpGet()]
		public IAsyncEnumerable<ITreeModel> TreeList(string super_id,string kw) 
		{
			return service.TreeListAsync(super_id,kw);
		}
		
	}
}
