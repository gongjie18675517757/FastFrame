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
			 var query = from _permission in permissionQueryable 
						join _parent_Id in permissionQueryable.MapTo<Permission,PermissionDto>() on _permission.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
					 select new PermissionDto
					{
						Parent_Id=_permission.Parent_Id,
						EnCode=_permission.EnCode,
						AreaName=_permission.AreaName,
						Name=_permission.Name,
						Id=_permission.Id,
						Parent=_parent_Id,
					};
			return query;
		}
		#endregion
	}
}
