	using FastFrame.Application.Basis; 
	using FastFrame.Application; 
	using Microsoft.AspNetCore.Mvc; 
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
		
		[HttpGet()]
		public IAsyncEnumerable<ITreeModel> TreeList(string super_id,string kw) 
		{
			return service.TreeListAsync(super_id,kw);
		}
		
	}
}
