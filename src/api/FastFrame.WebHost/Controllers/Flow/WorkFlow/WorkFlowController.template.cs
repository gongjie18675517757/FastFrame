namespace FastFrame.WebHost.Controllers.Flow
{
	using FastFrame.Entity.Flow; 
	using FastFrame.Application.Flow; 
	using FastFrame.Infrastructure.Permission; 
	using FastFrame.Infrastructure.Interface; 
		
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
