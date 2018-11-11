namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///登陆用户 
	/// </summary>
	public partial class UserController:BaseController<User, UserDto>
	{
		#region 字段
		private readonly UserService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public UserController(UserService service,IScopeServiceLoader serviceLoader)
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
