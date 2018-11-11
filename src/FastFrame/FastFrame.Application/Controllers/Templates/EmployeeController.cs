namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///员工表 
	/// </summary>
	public partial class EmployeeController:BaseController<Employee, EmployeeDto>
	{
		#region 字段
		private readonly EmployeeService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public EmployeeController(EmployeeService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		#endregion
	}
}
