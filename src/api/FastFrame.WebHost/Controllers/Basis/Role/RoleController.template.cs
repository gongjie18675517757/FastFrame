	using FastFrame.Application.Basis; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
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
