	using FastFrame.Application.Basis; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
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
		
		[HttpGet()]
		public IAsyncEnumerable<ITreeModel> TreeList(string super_id,string kw) 
		{
			return service.TreeListAsync(super_id,kw);
		}
		

	}
}
