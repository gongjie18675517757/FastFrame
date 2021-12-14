	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 组织信息 服务实现 
	/// </summary>
	public partial class TenantService:BaseService<Tenant, TenantDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Tenant> tenantRepository;
		
		public TenantService(IRepository<User> userRepository,IRepository<Tenant> tenantRepository)
			 : base(tenantRepository)
		{
			this.userRepository=userRepository;
			this.tenantRepository=tenantRepository;
		}
		
		protected override IQueryable<TenantDto> QueryMain() 
		{
			var repository = tenantRepository.Queryable;
			var query = from _tenant in repository 
						select new TenantDto
						{
							FullName = _tenant.FullName,
							ShortName = _tenant.ShortName,
							UrlMark = _tenant.UrlMark,
							Super_Id = _tenant.Super_Id,
							Tenant_Id = _tenant.Tenant_Id,
							HandIcon_Id = _tenant.HandIcon_Id,
							Id = _tenant.Id,
							ChildCount = repository.Count(c => c.Super_Id == _tenant.Id)
						};
			return query;
		}
		public Task<IPageList<TenantViewModel>> ViewModelListAsync(IPagination page) 
		{
			var query = tenantRepository.MapTo<Tenant, TenantViewModel>();
			return query.PageListAsync(page);
		}
		
	}
}
