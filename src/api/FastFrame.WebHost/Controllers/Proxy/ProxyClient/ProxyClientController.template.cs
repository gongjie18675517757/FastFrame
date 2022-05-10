	using FastFrame.Application.Proxy; 
namespace FastFrame.WebHost.Controllers.Proxy
{
		
	/// <summary>
	/// 内网穿透服务 
	/// </summary>
	public partial class ProxyClientController:BaseCURDController<ProxyClientDto>
	{
		private readonly ProxyClientService service;
		
		public ProxyClientController(ProxyClientService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
