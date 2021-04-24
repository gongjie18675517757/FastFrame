namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Application.Basis; 
	using FastFrame.Infrastructure.Permission; 
	using FastFrame.Infrastructure.Interface; 
		
	/// <summary>
	/// 角色 
	/// </summary>
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
