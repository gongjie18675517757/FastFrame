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
		#region 字段
		private readonly MenuService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public MenuController(MenuService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		#endregion
	}
}
