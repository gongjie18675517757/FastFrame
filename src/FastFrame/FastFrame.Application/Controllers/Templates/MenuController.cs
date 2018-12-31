namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///菜单 
	/// </summary>
	[Permission(nameof(Menu),"菜单")]
	public partial class MenuController:BaseController<Menu, MenuDto>
	{
		/*字段*/
		private readonly MenuService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		/*构造函数*/
		public MenuController(MenuService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		/*属性*/
		
		/*方法*/
		
	}
}
