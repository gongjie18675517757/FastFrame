namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using System.Linq; 
	/// <summary>
	///登陆用户 服务类 
	/// </summary>
	public partial class UserService:BaseService<User, UserDto>
	{
		#region 字段
		private readonly DeptRepository deptRepository;
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		#endregion
		#region 构造函数
		public UserService(DeptRepository deptRepository,ForeignRepository foreignRepository,UserRepository userRepository,IScopeServiceLoader loader)
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
			 var query = from user in userQueryable 
						join dept_Id in deptQueryable.MapTo<Dept,DeptDto>() on user.Dept_Id equals dept_Id.Id into t_dept_Id
						from dept_Id in t_dept_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on user.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new UserDto
					{
						Account=user.Account,
						Password=user.Password,
						Name=user.Name,
						Dept_Id=user.Dept_Id,
						Email=user.Email,
						PhoneNumber=user.PhoneNumber,
						HandIconId=user.HandIconId,
						IsAdmin=user.IsAdmin,
						IsDisabled=user.IsDisabled,
						Id=user.Id,
						Dept=dept_Id,
						CreateAccount = user2.Account,
						CreateName = user2.Name,
						CreateTime = foreing.CreateTime,
						ModifyAccount = user3.Account,
						ModifyName = user3.Name,
						ModifyTime = foreing.ModifyTime,
					};
			return query;
		}
		#endregion
	}
}
