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
	public partial class TenantService(IRepository<Tenant> tenantRepository,IServiceProvider loader):BaseService<Tenant, TenantDto>(loader,tenantRepository)
	{
		private readonly IRepository<Tenant> tenantRepository=tenantRepository;
		
		
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
		protected override IQueryable<ITreeModel> DefaultTreeModelQueryable(string kw) 
		{
			        var main_query = repository.Queryable;
        if (!kw.IsNullOrWhiteSpace())
        {
            var vm_query = repository.Select(Tenant.BuildExpression());
            main_query = main_query
                .Where(a => 
                         vm_query.Any(v => 
                            v.Value.Contains(kw) &&
                            repository.Any(x => x.Id == v.Id && x.TreeCode.StartsWith(a.TreeCode))));
        }

        return from a in main_query
               join b in repository.Select(Tenant.BuildExpression()) on a.Id equals b.Id 
               select new TreeModel
               {
                   Id = a.Id,
                   Super_Id = a.Super_Id,
                   Value = b.Value,
                   ChildCount = main_query.Count(v => v.Super_Id == a.Id),
                   TotalChildCount = main_query.Count(v => v.Id != a.Id && v.TreeCode.StartsWith(a.TreeCode)),
               };
		}
		
	}
}
