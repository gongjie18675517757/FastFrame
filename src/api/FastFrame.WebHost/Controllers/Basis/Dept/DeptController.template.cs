	using FastFrame.Application.Basis; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
	/// <summary>
	/// 部门 
	/// </summary>
	public partial class DeptController:BaseCURDController<DeptDto>
	{
		private readonly DeptService service;
		
		public DeptController(DeptService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
