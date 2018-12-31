namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///部门 服务类 
	/// </summary>
	public partial class DeptService:BaseService<Dept, DeptDto>
	{
		/*字段*/
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<Employee> employeeRepository;
		private readonly IRepository<User> userRepository;
		
		/*构造函数*/
		public DeptService(IRepository<Dept> deptRepository,IRepository<Employee> employeeRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(deptRepository,loader)
		{
			this.deptRepository=deptRepository;
			this.employeeRepository=employeeRepository;
			this.userRepository=userRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<DeptDto> QueryMain() 
		{
			 var deptQueryable = deptRepository.Queryable;
			 var employeeQueryable = employeeRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _dept in deptQueryable 
						join _parent_Id in deptQueryable.MapTo<Dept,DeptDto>() on _dept.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						join _supervisor_Id in employeeQueryable.MapTo<Employee,EmployeeDto>() on _dept.Supervisor_Id equals _supervisor_Id.Id into t__supervisor_Id
						from _supervisor_Id in t__supervisor_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _dept.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _dept.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
					 select new DeptDto
					{
						EnCode=_dept.EnCode,
						Name=_dept.Name,
						Parent_Id=_dept.Parent_Id,
						Supervisor_Id=_dept.Supervisor_Id,
						Id=_dept.Id,
						Create_User_Id=_dept.Create_User_Id,
						CreateTime=_dept.CreateTime,
						Modify_User_Id=_dept.Modify_User_Id,
						ModifyTime=_dept.ModifyTime,
						Parent=_parent_Id,
						Supervisor=_supervisor_Id,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
