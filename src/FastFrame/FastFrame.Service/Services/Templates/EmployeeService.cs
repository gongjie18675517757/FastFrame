namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///员工 服务类 
	/// </summary>
	public partial class EmployeeService:BaseService<Employee, EmployeeDto>
	{
		#region 字段
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<Employee> employeeRepository;
		#endregion
		#region 构造函数
		public EmployeeService(IRepository<User> userRepository,IRepository<Dept> deptRepository,IRepository<Foreign> foreignRepository,IRepository<Employee> employeeRepository,IScopeServiceLoader loader)
			:base(employeeRepository,loader)
		{
			this.userRepository=userRepository;
			this.deptRepository=deptRepository;
			this.foreignRepository=foreignRepository;
			this.employeeRepository=employeeRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<EmployeeDto> QueryMain() 
		{
			var employeeQueryable=employeeRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var deptQueryable = deptRepository.Queryable;
			 var query = from _employee in employeeQueryable 
						join _user_Id in userQueryable.MapTo<User,UserDto>() on _employee.User_Id equals _user_Id.Id into t__user_Id
						from _user_Id in t__user_Id.DefaultIfEmpty()
						join _dept_Id in deptQueryable.MapTo<Dept,DeptDto>() on _employee.Dept_Id equals _dept_Id.Id into t__dept_Id
						from _dept_Id in t__dept_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on _employee.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new EmployeeDto
					{
						EnCode=_employee.EnCode,
						Name=_employee.Name,
						Email=_employee.Email,
						PhoneNumber=_employee.PhoneNumber,
						Gender=_employee.Gender,
						User_Id=_employee.User_Id,
						Dept_Id=_employee.Dept_Id,
						Id=_employee.Id,
						User=_user_Id,
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
