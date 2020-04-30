namespace FastFrame.Application.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
		
	/// <summary>
	/// 部门 服务实现 
	/// </summary>
	public partial class DeptService:BaseService<Dept, DeptDto>
	{
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<User> userRepository;
		
		public DeptService(IRepository<Dept> deptRepository,IRepository<User> userRepository)
			 : base(deptRepository)
		{
			this.deptRepository=deptRepository;
			this.userRepository=userRepository;
		}
		
		protected override IQueryable<DeptDto> QueryMain() 
		{
			var deptQueryable = deptRepository.Queryable;
			var userQueryable = userRepository.Queryable;
			var query = from _dept in deptRepository 
						join _super_Id in deptQueryable on _dept.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _dept.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _dept.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						let Super = new DeptViewModel {Name = _super_Id.Name,Id = _super_Id.Id}
						let Create_User = new UserViewModel {Name = _create_User_Id.Name,Account = _create_User_Id.Account,Id = _create_User_Id.Id}
						let Modify_User = new UserViewModel {Name = _modify_User_Id.Name,Account = _modify_User_Id.Account,Id = _modify_User_Id.Id}
						select new DeptDto
						{
							EnCode=_dept.EnCode,
							Name=_dept.Name,
							Super_Id=_dept.Super_Id,
							Id=_dept.Id,
							Create_User_Id=_dept.Create_User_Id,
							CreateTime=_dept.CreateTime,
							Modify_User_Id=_dept.Modify_User_Id,
							ModifyTime=_dept.ModifyTime,
							Super=Super,
							Create_User=Create_User,
							Modify_User=Modify_User,
						};
			return query;
		}
		public  Task<PageList<DeptViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = from _dept in deptRepository 
						select new DeptViewModel
						{
							Name = _dept.Name,
							Id = _dept.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
