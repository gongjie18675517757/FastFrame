namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///权限 服务类 
	/// </summary>
	public partial class PermissionService:BaseService<Permission, PermissionDto>
	{
		#region 字段
		private readonly IRepository<Permission> permissionRepository;
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		#endregion
		#region 构造函数
		public PermissionService(IRepository<Permission> permissionRepository,IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
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
			 var permissionQueryable = permissionRepository.Queryable;
			 var query = from permission in permissionQueryable 
						join parent_Id in permissionQueryable.MapTo<Permission,PermissionDto>() on permission.Parent_Id equals parent_Id.Id into t_parent_Id
						from parent_Id in t_parent_Id.DefaultIfEmpty()
					 select new PermissionDto
					{
						Parent_Id=permission.Parent_Id,
						EnCode=permission.EnCode,
						AreaName=permission.AreaName,
						Name=permission.Name,
						Id=permission.Id,
						Parent=parent_Id,
					};
			return query;
		}
		#endregion
	}
}
