namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///用户 
	/// </summary>
	[Permission(nameof(User),"用户")]
	public partial class UserController:BaseCURDController<User, UserDto>
	{
		private readonly UserService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		public UserController(UserService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		
		
	}
}
