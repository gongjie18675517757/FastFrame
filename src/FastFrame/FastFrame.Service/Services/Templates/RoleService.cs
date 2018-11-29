namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///角色 服务类 
	/// </summary>
	public partial class RoleService:BaseService<Role, RoleDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Role> roleRepository;
		#endregion
		#region 构造函数
		public RoleService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<Role> roleRepository,IScopeServiceLoader loader)
			:base(roleRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.roleRepository=roleRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<RoleDto> QueryMain() 
		{
			var roleQueryable=roleRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var query = from role in roleQueryable 
					join foreing in foreignQueryable on role.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new RoleDto
					{
						EnCode=role.EnCode,
						Name=role.Name,
						Id=role.Id,
						Foreign = foreing,
						Create_User = user2,
						Modify_User = user3,
					};
			return query;
		}
		#endregion
	}
}
