namespace FastFrame.WebHost.Controllers.OA
{
	using FastFrame.Entity.OA;
	using FastFrame.Application.OA;
	using FastFrame.Infrastructure.Permission;
	using FastFrame.Infrastructure.Interface;

	/// <summary>
	/// 请假单 
	/// </summary>
	[Permission(nameof(OaLeave),"请假单")]
	public partial class OaLeaveController:BaseCURDController<OaLeaveDto>
	{
		private readonly OaLeaveService service;
		
		public OaLeaveController(OaLeaveService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
