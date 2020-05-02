namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Application.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
		
	/// <summary>
	/// 角色 
	/// </summary>
	[Permission(nameof(Role),"角色")]
	public partial class RoleController:BaseCURDController<RoleDto>
	{
		private readonly RoleService service;
		
		public RoleController(RoleService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
