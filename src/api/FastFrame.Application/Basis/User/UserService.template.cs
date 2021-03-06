	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 用户 服务实现 
	/// </summary>
	public partial class UserService:BaseService<User, UserDto>
	{
		private readonly IRepository<Resource> resourceRepository;
		private readonly IRepository<User> userRepository;
		
		public UserService(IRepository<Resource> resourceRepository,IRepository<User> userRepository)
			 : base(userRepository)
		{
			this.resourceRepository=resourceRepository;
			this.userRepository=userRepository;
		}
		
		protected override IQueryable<UserDto> QueryMain() 
		{
			var resourceQueryable = resourceRepository.Queryable.MapTo<Resource,ResourceViewModel>();
			var userQueryable = userRepository.Queryable.MapTo<User,UserViewModel>();
			var repository = userRepository.Queryable;
			var query = from _user in repository 
						join _handIcon_Id in resourceQueryable on _user.HandIcon_Id equals _handIcon_Id.Id into t__handIcon_Id
						from _handIcon_Id in t__handIcon_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _user.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _user.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new UserDto
						{
							Account = _user.Account,
							Password = _user.Password,
							Name = _user.Name,
							Email = _user.Email,
							PhoneNumber = _user.PhoneNumber,
							HandIcon_Id = _user.HandIcon_Id,
							IsAdmin = _user.IsAdmin,
							Enable = _user.Enable,
							Id = _user.Id,
							Create_User_Id = _user.Create_User_Id,
							CreateTime = _user.CreateTime,
							Modify_User_Id = _user.Modify_User_Id,
							ModifyTime = _user.ModifyTime,
							HandIcon = _handIcon_Id,
							Create_User = _create_User_Id,
							Modify_User = _modify_User_Id,
						};
			return query;
		}
		public Task<PageList<UserViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = userRepository.MapTo<User, UserViewModel>();
			return query.PageListAsync(page);
		}
		
	}
}
