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
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Role> roleRepository;
		
		public RoleService(IRepository<User> userRepository,IRepository<Role> roleRepository,IServiceProvider loader)
			 : base(loader,roleRepository)
		{
			this.userRepository=userRepository;
			this.roleRepository=roleRepository;
		}
		
		protected override IQueryable<RoleDto> DefaultQueryable() 
		{
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var repository = roleRepository.Queryable;
			var query = from _role in repository 
						join _create_User_Id in userQueryable on _role.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _role.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new RoleDto
						{
							EnCode = _role.EnCode,
							Name = _role.Name,
							IsDefault = _role.IsDefault,
							IsAdmin = _role.IsAdmin,
							Remarks = _role.Remarks,
							Id = _role.Id,
							Create_User_Id = _role.Create_User_Id,
							CreateTime = _role.CreateTime,
							Modify_User_Id = _role.Modify_User_Id,
							ModifyTime = _role.ModifyTime,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		
	}
}
