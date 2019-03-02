namespace FastFrame.Application.Controllers.Chat
{
	using FastFrame.Dto.Chat; 
	using FastFrame.Entity.Chat; 
	using FastFrame.Service.Services.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///通知 
	/// </summary>
	[Permission(nameof(Notify),"通知")]
	public partial class NotifyController:BaseController<Notify, NotifyDto>
	{
		private readonly NotifyService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		public NotifyController(NotifyService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		
		
	}
}
