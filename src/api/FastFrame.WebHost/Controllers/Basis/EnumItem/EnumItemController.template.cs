namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Application.Basis;
	using FastFrame.Entity.Basis;
	using FastFrame.Infrastructure.Permission;

	/// <summary>
	/// 数字字典 
	/// </summary>
	[Permission(nameof(EnumItem),"数字字典")]
	public partial class EnumItemController:BaseCURDController<EnumItemDto>
	{
		private readonly EnumItemService service;
		
		public EnumItemController(EnumItemService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
