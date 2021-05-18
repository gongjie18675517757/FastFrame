	using FastFrame.Application.Basis; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
	/// <summary>
	/// 通知 
	/// </summary>
	public partial class NotifyController:BaseCURDController<NotifyDto>
	{
		private readonly NotifyService service;
		
		public NotifyController(NotifyService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
