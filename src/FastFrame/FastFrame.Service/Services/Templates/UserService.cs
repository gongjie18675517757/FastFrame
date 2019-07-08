namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	/// <summary>
	///用户 服务类 
	/// </summary>
	public partial class UserService:BaseService<User, UserDto>
	{
		private readonly IRepository<Resource> resourceRepository;
		private readonly IRepository<User> userRepository;
		
		public UserService(IRepository<Resource> resourceRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(userRepository,loader)
		{
			this.resourceRepository=resourceRepository;
			this.userRepository=userRepository;
		}
		
		
		protected override IQueryable<UserDto> QueryMain() 
		{
			 var resourceQueryable = resourceRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _user in userQueryable 
						join _handIcon_Id in resourceQueryable on _user.HandIcon_Id equals _handIcon_Id.Id into t__handIcon_Id
						from _handIcon_Id in t__handIcon_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _user.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _user.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						 select new UserDto
						{
							Account=_user.Account,
							Password=_user.Password,
							Name=_user.Name,
							Email=_user.Email,
							PhoneNumber=_user.PhoneNumber,
							HandIcon_Id=_user.HandIcon_Id,
							IsAdmin=_user.IsAdmin,
							IsDisabled=_user.IsDisabled,
							Id=_user.Id,
							Create_User_Id=_user.Create_User_Id,
							CreateTime=_user.CreateTime,
							Modify_User_Id=_user.Modify_User_Id,
							ModifyTime=_user.ModifyTime,
							HandIcon=new ResourceViewModel
							{
								Id = _handIcon_Id.Id,
								Name = _handIcon_Id.Name,
								Size = _handIcon_Id.Size,
								Path = _handIcon_Id.Path,
								ContentType = _handIcon_Id.ContentType,
								MD5 = _handIcon_Id.MD5,
							},
							Create_User=new UserViewModel
							{
								Id = _create_User_Id.Id,
								Name = _create_User_Id.Name,
								Account = _create_User_Id.Account,
							},
							Modify_User=new UserViewModel
							{
								Id = _modify_User_Id.Id,
								Name = _modify_User_Id.Name,
								Account = _modify_User_Id.Account,
							},
					};
			return query;
		}
		
	}
}
