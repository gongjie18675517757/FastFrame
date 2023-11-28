	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 部门 服务实现 
	/// </summary>
	public partial class DeptService(IRepository<Dept> deptRepository,IRepository<User> userRepository,IServiceProvider loader):BaseService<Dept, DeptDto>(loader,deptRepository)
	{
		private readonly IRepository<Dept> deptRepository=deptRepository;
		private readonly IRepository<User> userRepository=userRepository;
		
		
		protected override IQueryable<DeptDto> DefaultQueryable() 
		{
			var deptQueryable = deptRepository.Queryable.Select(Dept.BuildExpression());
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var repository = deptRepository.Queryable;
			var query = from _dept in repository 
						join _super_Id in deptQueryable on _dept.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _dept.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _dept.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new DeptDto
						{
							Super_Id = _dept.Super_Id,
							EnCode = _dept.EnCode,
							Name = _dept.Name,
							Remarks = _dept.Remarks,
							Id = _dept.Id,
							Create_User_Id = _dept.Create_User_Id,
							CreateTime = _dept.CreateTime,
							Modify_User_Id = _dept.Modify_User_Id,
							ModifyTime = _dept.ModifyTime,
							Super_Value = _super_Id.Value,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		protected override IQueryable<Entity.IViewModel> DefaultViewModelQueryable() 
		{
			return repository.Select(Dept.BuildExpression());
		}
		protected override IQueryable<ITreeModel> DefaultTreeModelQueryable(string kw) 
		{
			        var main_query = repository.Queryable;
        if (!kw.IsNullOrWhiteSpace())
        {
            var vm_query = repository.Select(Dept.BuildExpression());
            main_query = main_query
                .Where(a => 
                         vm_query.Any(v => 
                            v.Value.Contains(kw) &&
                            repository.Any(x => x.Id == v.Id && x.TreeCode.StartsWith(a.TreeCode))));
        }

        return from a in main_query
               join b in repository.Select(Dept.BuildExpression()) on a.Id equals b.Id 
               select new TreeModel
               {
                   Id = a.Id,
                   Super_Id = a.Super_Id,
                   Value = b.Value,
                   ChildCount = main_query.Count(v => v.Super_Id == a.Id),
                   TotalChildCount = main_query.Count(v => v.Id != a.Id && v.TreeCode.StartsWith(a.TreeCode)),
               };
		}
		
	}
}
