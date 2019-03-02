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
		private readonly RoleService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		public RoleController(RoleService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		
		
	}
}
