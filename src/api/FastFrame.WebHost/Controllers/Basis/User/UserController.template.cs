namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Application.Basis; 
	using FastFrame.Infrastructure.Permission; 
	using FastFrame.Infrastructure.Interface; 
		
	/// <summary>
	/// 用户 
	/// </summary>
	public partial class UserController:BaseCURDController<UserDto>
	{
		private readonly UserService service;
		
		public UserController(UserService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
