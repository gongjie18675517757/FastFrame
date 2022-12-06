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
	/// 角色 服务实现 
	/// </summary>
	public partial class RoleService:BaseService<Role, RoleDto>
	{
		private readonly IRepository<Role> roleRepository;
		private readonly IRepository<User> userRepository;
		
		public RoleService(IRepository<Role> roleRepository,IRepository<User> userRepository,IServiceProvider loader)
			 : base(loader,roleRepository)
		{
			this.roleRepository=roleRepository;
			this.userRepository=userRepository;
		}
		
		protected override IQueryable<RoleDto> DefaultQueryable() 
		{
			var roleQueryable = roleRepository.Queryable.Select(Role.BuildExpression());
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var repository = roleRepository.Queryable;
			var query = from _role in repository 
						join _super_Id in roleQueryable on _role.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _role.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _role.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new RoleDto
						{
							EnCode = _role.EnCode,
							Name = _role.Name,
							Super_Id = _role.Super_Id,
							IsDefault = _role.IsDefault,
							IsAdmin = _role.IsAdmin,
							Remarks = _role.Remarks,
							Id = _role.Id,
							Create_User_Id = _role.Create_User_Id,
							CreateTime = _role.CreateTime,
							Modify_User_Id = _role.Modify_User_Id,
							ModifyTime = _role.ModifyTime,
							Super_Value = _super_Id.Value,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		protected override IQueryable<Entity.IViewModel> DefaultViewModelQueryable() 
		{
			return repository.Select(Role.BuildExpression());
		}
		protected override IQueryable<ITreeModel> DefaultTreeModelQueryable() 
		{
			return from a in repository
					join b in repository.Select(Role.BuildExpression()) on a.Id equals b.Id
					select new TreeModel
					{
					Id = a.Id,
					Super_Id = a.Super_Id,
					Value = b.Value,
					ChildCount = repository.Count(v => v.Super_Id == a.Id),
					TotalChildCount = repository.Count(v => v.Id != a.Id && v.TreeCode.StartsWith(a.TreeCode)),
				};
		}
		
	}
}
