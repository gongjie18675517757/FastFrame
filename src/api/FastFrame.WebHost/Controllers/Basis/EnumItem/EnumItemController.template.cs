	using FastFrame.Application.Basis; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
	/// <summary>
	/// 数字字典 
	/// </summary>
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
