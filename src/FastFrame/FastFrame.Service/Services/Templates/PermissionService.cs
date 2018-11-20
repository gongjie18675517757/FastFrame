namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using System.Linq; 
	/// <summary>
	///权限 服务类 
	/// </summary>
	public partial class PermissionService:BaseService<Permission, PermissionDto>
	{
		#region 字段
		private readonly PermissionRepository permissionRepository;
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		#endregion
		#region 构造函数
		public PermissionService(PermissionRepository permissionRepository,ForeignRepository foreignRepository,UserRepository userRepository,IScopeServiceLoader loader)
			:base(permissionRepository,loader)
		{
			this.permissionRepository=permissionRepository;
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<PermissionDto> QueryMain() 
		{
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var permissionQueryable = permissionRepository.Queryable;
			 var query = from permission in permissionQueryable 
						join parent_Id in permissionQueryable.MapTo<Permission,PermissionDto>() on permission.Parent_Id equals parent_Id.Id into t_parent_Id
						from parent_Id in t_parent_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on permission.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new PermissionDto
					{
						Parent_Id=permission.Parent_Id,
						EnCode=permission.EnCode,
						AreaName=permission.AreaName,
						Name=permission.Name,
						Id=permission.Id,
						Parent=parent_Id,
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
