namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///数字字典 
	/// </summary>
	[Permission(nameof(EnumItem),"数字字典")]
	public partial class EnumItemController:BaseCURDController<EnumItem, EnumItemDto>
	{
		private readonly EnumItemService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		public EnumItemController(EnumItemService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		
		
	}
}
