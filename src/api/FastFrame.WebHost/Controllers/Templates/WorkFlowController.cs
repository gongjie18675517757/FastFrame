namespace FastFrame.WebHost.Controllers.Flow
{
	using FastFrame.Dto.Flow; 
	using FastFrame.Entity.Flow; 
	using FastFrame.Service.Services.Flow; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///工作流 
	/// </summary>
	[Permission(nameof(WorkFlow),"工作流")]
	public partial class WorkFlowController:BaseCURDController<WorkFlow, WorkFlowDto>
	{
		private readonly WorkFlowService service;
		
		public WorkFlowController(WorkFlowService service)
			:base(service)
		{
			this.service = service;
		}
		
		
		
	}
}
