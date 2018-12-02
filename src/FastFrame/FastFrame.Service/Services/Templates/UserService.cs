namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///用户 服务类 
	/// </summary>
	public partial class UserService:BaseService<User, UserDto>
	{
		#region 字段
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		#endregion
		#region 构造函数
		public UserService(IRepository<Dept> deptRepository,IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(userRepository,loader)
		{
			this.deptRepository=deptRepository;
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<UserDto> QueryMain() 
		{
			var userQueryable=userRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var deptQueryable = deptRepository.Queryable;
			 var query = from _user in userQueryable 
						join _dept_Id in deptQueryable.MapTo<Dept,DeptDto>() on _user.Dept_Id equals _dept_Id.Id into t__dept_Id
						from _dept_Id in t__dept_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on _user.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new UserDto
					{
						Account=_user.Account,
						Password=_user.Password,
						Name=_user.Name,
						Dept_Id=_user.Dept_Id,
						Email=_user.Email,
						PhoneNumber=_user.PhoneNumber,
						HandIconId=_user.HandIconId,
						IsAdmin=_user.IsAdmin,
						IsDisabled=_user.IsDisabled,
						Id=_user.Id,
						Dept=_dept_Id,
						Foreign = foreing,
						Create_User = user2,
						Modify_User = user3,
					};
			return query;
		}
		#endregion
	}
}
