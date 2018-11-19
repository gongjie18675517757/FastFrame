namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///权限 
	/// </summary>
	[Permission(nameof(Permission),"权限")]
	public partial class PermissionController:BaseController<Permission, PermissionDto>
	{
		#region 字段
		private readonly PermissionService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public PermissionController(PermissionService service,IScopeServiceLoader serviceLoader)
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
