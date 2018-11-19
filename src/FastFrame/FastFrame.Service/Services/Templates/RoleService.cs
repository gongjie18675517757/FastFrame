namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using System.Linq; 
	/// <summary>
	///角色 服务类 
	/// </summary>
	public partial class RoleService:BaseService<Role, RoleDto>
	{
		#region 字段
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly RoleRepository roleRepository;
		#endregion
		#region 构造函数
		public RoleService(ForeignRepository foreignRepository,UserRepository userRepository,RoleRepository roleRepository,IScopeServiceLoader loader)
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
