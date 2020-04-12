namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	/// <summary>
	///权限 服务实现 
	/// </summary>
	public partial class PermissionService:BaseService<Permission, PermissionDto>
	{
		private readonly IRepository<Permission> permissionRepository;
		private readonly IRepository<User> userRepository;
		
		public PermissionService(IRepository<Permission> permissionRepository,IRepository<User> userRepository)
			:base(permissionRepository)
		{
			this.permissionRepository=permissionRepository;
			this.userRepository=userRepository;
		}
		
		
		protected override IQueryable<PermissionDto> QueryMain() 
		{
			var permissionQueryable = permissionRepository.Queryable;
			var query = from _permission in permissionRepository 
						join _super_Id in permissionQueryable on _permission.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						let Super=new PermissionViewModel {Name=_super_Id.Name,Id=_super_Id.Id}
						 select new PermissionDto
						{
							Name=_permission.Name,
							EnCode=_permission.EnCode,
							AreaName=_permission.AreaName,
							Super_Id=_permission.Super_Id,
							Id=_permission.Id,
							Super=Super,
					};
			return query;
		}
		public  Task<PageList<PermissionViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = from _permission in permissionRepository 
						select new PermissionViewModel
						{
							Name = _permission.Name,
							Id = _permission.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
