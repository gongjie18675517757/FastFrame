namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///部门 
	/// </summary>
	[Permission(nameof(Dept),"部门")]
	public partial class DeptController:BaseController<Dept, DeptDto>
	{
		/*字段*/
		private readonly DeptService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		/*构造函数*/
		public DeptController(DeptService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		/*属性*/
		
		/*方法*/
		
	}
}
