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
	///部门 服务实现 
	/// </summary>
	public partial class DeptService:BaseService<Dept, DeptDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Dept> deptRepository;
		
		public DeptService(IRepository<User> userRepository,IRepository<Dept> deptRepository,IScopeServiceLoader loader)
			:base(deptRepository,loader)
		{
			this.userRepository=userRepository;
			this.deptRepository=deptRepository;
		}
		
		
		protected override IQueryable<DeptDto> QueryMain() 
		{
			 var userQueryable = userRepository.Queryable;
			 var deptQueryable = deptRepository.Queryable;
			 var query = from _dept in deptRepository 
						join _supervisor_Id in userQueryable on _dept.Supervisor_Id equals _supervisor_Id.Id into t__supervisor_Id
						from _supervisor_Id in t__supervisor_Id.DefaultIfEmpty()
						join _super_Id in deptQueryable on _dept.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _dept.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _dept.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						let Supervisor=new UserViewModel {Name=_supervisor_Id.Name,Account=_supervisor_Id.Account,Id=_supervisor_Id.Id}
						let Super=new DeptViewModel {Name=_super_Id.Name,EnCode=_super_Id.EnCode,Id=_super_Id.Id}
						let Create_User=new UserViewModel {Name=_create_User_Id.Name,Account=_create_User_Id.Account,Id=_create_User_Id.Id}
						let Modify_User=new UserViewModel {Name=_modify_User_Id.Name,Account=_modify_User_Id.Account,Id=_modify_User_Id.Id}
						 select new DeptDto
						{
							EnCode=_dept.EnCode,
							Name=_dept.Name,
							Supervisor_Id=_dept.Supervisor_Id,
							Super_Id=_dept.Super_Id,
							Id=_dept.Id,
							Create_User_Id=_dept.Create_User_Id,
							CreateTime=_dept.CreateTime,
							Modify_User_Id=_dept.Modify_User_Id,
							ModifyTime=_dept.ModifyTime,
							Supervisor=Supervisor,
							Super=Super,
							Create_User=Create_User,
							Modify_User=Modify_User,
					};
			return query;
		}
		
	}
}
