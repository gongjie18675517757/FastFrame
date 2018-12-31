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
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<Employee> employeeRepository;
		
		/*构造函数*/
		public EmployeeService(IRepository<User> userRepository,IRepository<Dept> deptRepository,IRepository<Employee> employeeRepository,IScopeServiceLoader loader)
			:base(employeeRepository,loader)
		{
			this.userRepository=userRepository;
			this.deptRepository=deptRepository;
			this.employeeRepository=employeeRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<EmployeeDto> QueryMain() 
		{
			var employeeQueryable=employeeRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var deptQueryable = deptRepository.Queryable;
			 var query = from _employee in employeeQueryable 
						join _user_Id in userQueryable.MapTo<User,UserDto>() on _employee.User_Id equals _user_Id.Id into t__user_Id
						from _user_Id in t__user_Id.DefaultIfEmpty()
						join _dept_Id in deptQueryable.MapTo<Dept,DeptDto>() on _employee.Dept_Id equals _dept_Id.Id into t__dept_Id
						from _dept_Id in t__dept_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _employee.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _employee.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
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
						Create_User_Id=_employee.Create_User_Id,
						CreateTime=_employee.CreateTime,
						Modify_User_Id=_employee.Modify_User_Id,
						ModifyTime=_employee.ModifyTime,
						User=_user_Id,
						Dept=_dept_Id,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
