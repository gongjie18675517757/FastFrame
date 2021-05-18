	using FastFrame.Application.Flow; 
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
		
		
	}
}
