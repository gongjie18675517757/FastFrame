namespace FastFrame.WebHost.Controllers.Basis
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
		
		public EnumItemController(EnumItemService service)
			:base(service)
		{
			this.service = service;
		}
		
		
		
	}
}
