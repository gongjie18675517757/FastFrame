	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Proxy
{
		
	/// <summary>
	/// 内网穿透服务 
	/// </summary>
	public partial class ProxyClientViewModel:IViewModel
	{
		
		protected ProxyClientViewModel()
		{
		}
		
		/// <summary>
		/// 名称 
		/// </summary>
		public string Name {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
