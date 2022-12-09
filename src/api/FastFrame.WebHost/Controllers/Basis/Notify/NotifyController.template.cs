	using FastFrame.Application.Basis; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
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
		
		[HttpGet("{super_id?}")]
		public IAsyncEnumerable<ITreeModel> TreeList(string super_id) 
		{
			return service.TreeModelListAsync(super_id);
		}
		
	}
}
