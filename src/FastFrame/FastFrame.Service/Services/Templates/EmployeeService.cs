namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using System.Linq; 
	/// <summary>
	///员工表 服务类 
	/// </summary>
	public partial class EmployeeService:BaseService<Employee, EmployeeDto>
	{
		#region 字段
		private readonly UserRepository userRepository;
		private readonly DeptRepository deptRepository;
		private readonly ForeignRepository foreignRepository;
		private readonly EmployeeRepository employeeRepository;
		#endregion
		#region 构造函数
		public EmployeeService(UserRepository userRepository,DeptRepository deptRepository,ForeignRepository foreignRepository,EmployeeRepository employeeRepository,IScopeServiceLoader loader)
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
			 var query = from employee in employeeQueryable 
						join user_Id in userQueryable on employee.User_Id equals user_Id.Id into t_user_Id
						from user_Id in t_user_Id.DefaultIfEmpty()
						join dept_Id in deptQueryable on employee.Dept_Id equals dept_Id.Id into t_dept_Id
						from dept_Id in t_dept_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on employee.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new EmployeeDto
					{
						EnCode=employee.EnCode,
						Name=employee.Name,
						Email=employee.Email,
						PhoneNumber=employee.PhoneNumber,
						Gender=employee.Gender,
						User_Id=employee.User_Id,
						Dept_Id=employee.Dept_Id,
						Id=employee.Id,
						User_Account=user_Id.Account,
						User_Name=user_Id.Name,
						Dept_EnCode=dept_Id.EnCode,
						Dept_Name=dept_Id.Name,
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
