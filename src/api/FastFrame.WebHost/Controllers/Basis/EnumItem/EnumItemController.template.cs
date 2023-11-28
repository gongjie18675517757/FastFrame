	using FastFrame.Application.Basis; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
	/// <summary>
	/// 数字字典 
	/// </summary>
	public partial class EnumItemController(EnumItemService service):BaseCURDController<EnumItemDto>(service)
	{
		private readonly EnumItemService service=service;
		
		
		[HttpGet()]
		public IAsyncEnumerable<ITreeModel> TreeList(string super_id,string kw) 
		{
			return service.TreeListAsync(super_id,kw);
		}
		
	}
}
