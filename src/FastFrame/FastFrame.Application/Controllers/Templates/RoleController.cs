namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///角色 
	/// </summary>
	[Permission(nameof(Role),"角色")]
	public partial class RoleController:BaseController<Role, RoleDto>
	{
		/*字段*/
		private readonly RoleService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		/*构造函数*/
		public RoleController(RoleService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		/*属性*/
		
		/*方法*/
		
	}
}
