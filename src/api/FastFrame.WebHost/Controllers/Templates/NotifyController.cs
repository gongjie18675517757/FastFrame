namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///通知 
	/// </summary>
	[Permission(nameof(Notify),"通知")]
	public partial class NotifyController:BaseCURDController<Notify, NotifyDto>
	{
		private readonly NotifyService service;
		
		public NotifyController(NotifyService service)
			:base(service)
		{
			this.service = service;
		}
		
		
		
	}
}