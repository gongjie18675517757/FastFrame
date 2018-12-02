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
		#region 字段
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<Employee> employeeRepository;
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		#endregion
		#region 构造函数
		public DeptService(IRepository<Dept> deptRepository,IRepository<Employee> employeeRepository,IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(deptRepository,loader)
		{
			this.deptRepository=deptRepository;
			this.employeeRepository=employeeRepository;
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<DeptDto> QueryMain() 
		{
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var deptQueryable = deptRepository.Queryable;
			 var employeeQueryable = employeeRepository.Queryable;
			 var query = from _dept in deptQueryable 
						join _parent_Id in deptQueryable.MapTo<Dept,DeptDto>() on _dept.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						join _supervisor_Id in employeeQueryable.MapTo<Employee,EmployeeDto>() on _dept.Supervisor_Id equals _supervisor_Id.Id into t__supervisor_Id
						from _supervisor_Id in t__supervisor_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on _dept.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new DeptDto
					{
						EnCode=_dept.EnCode,
						Name=_dept.Name,
						Parent_Id=_dept.Parent_Id,
						Supervisor_Id=_dept.Supervisor_Id,
						Id=_dept.Id,
						Parent=_parent_Id,
						Supervisor=_supervisor_Id,
						Foreign = foreing,
						Create_User = user2,
						Modify_User = user3,
					};
			return query;
		}
		#endregion
	}
}
