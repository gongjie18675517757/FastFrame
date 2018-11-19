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
		#region 字段
		private readonly DeptService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public DeptController(DeptService service,IScopeServiceLoader serviceLoader)
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
