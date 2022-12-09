	using FastFrame.Application.Flow; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
namespace FastFrame.WebHost.Controllers.Flow
{
		
	/// <summary>
	/// 工作流 
	/// </summary>
	public partial class WorkFlowController:BaseCURDController<WorkFlowDto>
	{
		private readonly WorkFlowService service;
		
		public WorkFlowController(WorkFlowService service)
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
