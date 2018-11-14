namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using System.Linq; 
	/// <summary>
	///部门 服务类 
	/// </summary>
	public partial class DeptService:BaseService<Dept, DeptDto>
	{
		#region 字段
		private readonly DeptRepository deptRepository;
		private readonly EmployeeRepository employeeRepository;
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		#endregion
		#region 构造函数
		public DeptService(DeptRepository deptRepository,EmployeeRepository employeeRepository,ForeignRepository foreignRepository,UserRepository userRepository,IScopeServiceLoader loader)
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
			 var query = from dept in deptQueryable 
						join parent_Id in deptQueryable on dept.Parent_Id equals parent_Id.Id into t_parent_Id
						from parent_Id in t_parent_Id.DefaultIfEmpty()
						join supervisor_Id in employeeQueryable on dept.Supervisor_Id equals supervisor_Id.Id into t_supervisor_Id
						from supervisor_Id in t_supervisor_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on dept.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new DeptDto
					{
						EnCode=dept.EnCode,
						Name=dept.Name,
						Parent_Id=dept.Parent_Id,
						Supervisor_Id=dept.Supervisor_Id,
						Id=dept.Id,
						Parent_EnCode=parent_Id.EnCode,
						Parent_Name=parent_Id.Name,
						Supervisor_EnCode=supervisor_Id.EnCode,
						Supervisor_Name=supervisor_Id.Name,
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
