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
	///员工 服务类 
	/// </summary>
	public partial class EmployeeService:BaseService<Employee, EmployeeDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<Employee> employeeRepository;
		
		public EmployeeService(IRepository<User> userRepository,IRepository<Dept> deptRepository,IRepository<Employee> employeeRepository,IScopeServiceLoader loader)
			:base(employeeRepository,loader)
		{
			this.userRepository=userRepository;
			this.deptRepository=deptRepository;
			this.employeeRepository=employeeRepository;
		}
		
		
		protected override IQueryable<EmployeeDto> QueryMain() 
		{
			var employeeQueryable=employeeRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var deptQueryable = deptRepository.Queryable;
			 var query = from _employee in employeeQueryable 
						join _user_Id in userQueryable.TagWith("_user_Id") on _employee.User_Id equals _user_Id.Id into t__user_Id
						from _user_Id in t__user_Id.DefaultIfEmpty()
						join _dept_Id in deptQueryable.TagWith("_dept_Id") on _employee.Dept_Id equals _dept_Id.Id into t__dept_Id
						from _dept_Id in t__dept_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.TagWith("_create_User_Id") on _employee.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.TagWith("_modify_User_Id") on _employee.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
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
							User=_user_Id==null?null:new UserDto
							{
								Id = _user_Id.Id,
								Account = _user_Id.Account,
								Password = _user_Id.Password,
								Name = _user_Id.Name,
								Email = _user_Id.Email,
								PhoneNumber = _user_Id.PhoneNumber,
								HandIcon_Id = _user_Id.HandIcon_Id,
								HandIcon = null,
								IsAdmin = _user_Id.IsAdmin,
								IsDisabled = _user_Id.IsDisabled,
								Create_User_Id = _user_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _user_Id.CreateTime,
								Modify_User_Id = _user_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _user_Id.ModifyTime,
							},
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
							Create_User=_create_User_Id==null?null:new UserDto
							{
								Id = _create_User_Id.Id,
								Account = _create_User_Id.Account,
								Password = _create_User_Id.Password,
								Name = _create_User_Id.Name,
								Email = _create_User_Id.Email,
								PhoneNumber = _create_User_Id.PhoneNumber,
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
