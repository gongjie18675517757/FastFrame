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
		/*字段*/
		private readonly PermissionService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		/*构造函数*/
		public PermissionController(PermissionService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		/*属性*/
		
		/*方法*/
		
	}
}
