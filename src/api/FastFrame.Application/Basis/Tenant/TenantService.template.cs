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
	/// 多租户信息 服务实现 
	/// </summary>
	public partial class TenantService:BaseService<Tenant, TenantDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Tenant> tenantRepository;
		
		public TenantService(IRepository<User> userRepository,IRepository<Tenant> tenantRepository,IServiceProvider loader)
			 : base(loader,tenantRepository)
		{
			this.userRepository=userRepository;
			this.tenantRepository=tenantRepository;
		}
		
		protected override IQueryable<TenantDto> DefaultQueryable() 
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
						};
			return query;
		}
		protected override IQueryable<Entity.IViewModel> DefaultViewModelQueryable() 
		{
			return repository.Select(Tenant.BuildExpression());
		}
		protected override IQueryable<ITreeModel> DefaultTreeModelQueryable() 
		{
			return from a in repository
					join b in repository.Select(Tenant.BuildExpression()) on a.Id equals b.Id
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
