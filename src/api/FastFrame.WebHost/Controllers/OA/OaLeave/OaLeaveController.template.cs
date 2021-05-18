	using FastFrame.Application.OA; 
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
		
		
	}
}
