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
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<Resource> resourceRepository;
		private readonly IRepository<User> userRepository;
		
		public UserService(IRepository<Dept> deptRepository,IRepository<Resource> resourceRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(userRepository,loader)
		{
			this.deptRepository=deptRepository;
			this.resourceRepository=resourceRepository;
			this.userRepository=userRepository;
		}
		
		
		protected override IQueryable<UserDto> QueryMain() 
		{
			 var deptQueryable = deptRepository.Queryable;
			 var resourceQueryable = resourceRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _user in userQueryable 
						join _dept_Id in deptQueryable.TagWith("_dept_Id") on _user.Dept_Id equals _dept_Id.Id into t__dept_Id
						from _dept_Id in t__dept_Id.DefaultIfEmpty()
						join _handIcon_Id in resourceQueryable.TagWith("_handIcon_Id") on _user.HandIcon_Id equals _handIcon_Id.Id into t__handIcon_Id
						from _handIcon_Id in t__handIcon_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.TagWith("_create_User_Id") on _user.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.TagWith("_modify_User_Id") on _user.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						 select new UserDto
						{
							Account=_user.Account,
							Password=_user.Password,
							Name=_user.Name,
							Email=_user.Email,
							PhoneNumber=_user.PhoneNumber,
							Dept_Id=_user.Dept_Id,
							HandIcon_Id=_user.HandIcon_Id,
							IsAdmin=_user.IsAdmin,
							IsDisabled=_user.IsDisabled,
							Id=_user.Id,
							Create_User_Id=_user.Create_User_Id,
							CreateTime=_user.CreateTime,
							Modify_User_Id=_user.Modify_User_Id,
							ModifyTime=_user.ModifyTime,
							Dept=_dept_Id==null?null:new DeptDto
							{
								Id = _dept_Id.Id,
								EnCode = _dept_Id.EnCode,
								Name = _dept_Id.Name,
								Parent_Id = _dept_Id.Parent_Id,
								Parent = null,
								Supervisor_Id = _dept_Id.Supervisor_Id,
								Supervisor = null,
								Create_User_Id = _dept_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _dept_Id.CreateTime,
								Modify_User_Id = _dept_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _dept_Id.ModifyTime,
							},
							HandIcon=_handIcon_Id==null?null:new ResourceDto
							{
								Id = _handIcon_Id.Id,
								Name = _handIcon_Id.Name,
								Size = _handIcon_Id.Size,
								Path = _handIcon_Id.Path,
								ContentType = _handIcon_Id.ContentType,
								MD5 = _handIcon_Id.MD5,
							},
							Create_User=_create_User_Id==null?null:new UserDto
							{
								Id = _create_User_Id.Id,
								Account = _create_User_Id.Account,
								Password = _create_User_Id.Password,
								Name = _create_User_Id.Name,
								Email = _create_User_Id.Email,
								PhoneNumber = _create_User_Id.PhoneNumber,
								Dept_Id = _create_User_Id.Dept_Id,
								Dept = null,
								HandIcon_Id = _create_User_Id.HandIcon_Id,
								HandIcon = null,
								IsAdmin = _create_User_Id.IsAdmin,
								IsDisabled = _create_User_Id.IsDisabled,
								Create_User_Id = _create_User_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _create_User_Id.CreateTime,
								Modify_User_Id = _create_User_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _create_User_Id.ModifyTime,
							},
							Modify_User=_modify_User_Id==null?null:new UserDto
							{
								Id = _modify_User_Id.Id,
								Account = _modify_User_Id.Account,
								Password = _modify_User_Id.Password,
								Name = _modify_User_Id.Name,
								Email = _modify_User_Id.Email,
								PhoneNumber = _modify_User_Id.PhoneNumber,
								Dept_Id = _modify_User_Id.Dept_Id,
								Dept = null,
								HandIcon_Id = _modify_User_Id.HandIcon_Id,
								HandIcon = null,
								IsAdmin = _modify_User_Id.IsAdmin,
								IsDisabled = _modify_User_Id.IsDisabled,
								Create_User_Id = _modify_User_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _modify_User_Id.CreateTime,
								Modify_User_Id = _modify_User_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _modify_User_Id.ModifyTime,
							},
					};
			return query;
		}
		
	}
}
