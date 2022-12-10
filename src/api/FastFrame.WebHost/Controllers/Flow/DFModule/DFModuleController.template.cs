	using FastFrame.Application.Flow; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
namespace FastFrame.WebHost.Controllers.Flow
{
		
	/// <summary>
	/// 动态表单模块 
	/// </summary>
	public partial class DFModuleController:BaseCURDController<DFModuleDto>
	{
		private readonly DFModuleService service;
		
		public DFModuleController(DFModuleService service)
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
