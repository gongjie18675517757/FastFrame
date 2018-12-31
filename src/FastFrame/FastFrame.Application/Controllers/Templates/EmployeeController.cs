namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///员工 
	/// </summary>
	[Permission(nameof(Employee),"员工")]
	public partial class EmployeeController:BaseController<Employee, EmployeeDto>
	{
		/*字段*/
		private readonly EmployeeService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		/*构造函数*/
		public EmployeeController(EmployeeService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		/*属性*/
		
		/*方法*/
		
	}
}
