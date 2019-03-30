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
	///权限 服务类 
	/// </summary>
	public partial class PermissionService:BaseService<Permission, PermissionDto>
	{
		private readonly IRepository<Permission> permissionRepository;
		private readonly IRepository<User> userRepository;
		
		public PermissionService(IRepository<Permission> permissionRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(permissionRepository,loader)
		{
			this.permissionRepository=permissionRepository;
			this.userRepository=userRepository;
		}
		
		
		protected override IQueryable<PermissionDto> QueryMain() 
		{
			 var permissionQueryable = permissionRepository.Queryable;
			 var query = from _permission in permissionQueryable 
						join _parent_Id in permissionQueryable.TagWith("_parent_Id") on _permission.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						 select new PermissionDto
						{
							Name=_permission.Name,
							EnCode=_permission.EnCode,
							AreaName=_permission.AreaName,
							Parent_Id=_permission.Parent_Id,
							Id=_permission.Id,
							Parent=_parent_Id==null?null:new PermissionDto
							{
								Id = _parent_Id.Id,
								Name = _parent_Id.Name,
								EnCode = _parent_Id.EnCode,
								AreaName = _parent_Id.AreaName,
								Parent_Id = _parent_Id.Parent_Id,
								Parent = null,
							},
					};
			return query;
		}
		
	}
}
