	using FastFrame.Application.Flow; 
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
		
		
	}
}
