	using FastFrame.Application.Basis; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
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
