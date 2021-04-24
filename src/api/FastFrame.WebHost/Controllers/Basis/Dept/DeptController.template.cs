namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Application.Basis; 
	using FastFrame.Infrastructure.Permission; 
	using FastFrame.Infrastructure.Interface; 
		
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
