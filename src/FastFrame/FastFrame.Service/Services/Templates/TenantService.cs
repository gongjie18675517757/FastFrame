namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///组织信息 服务类 
	/// </summary>
	public partial class TenantService:BaseService<Tenant, TenantDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Tenant> tenantRepository;
		#endregion
		#region 构造函数
		public TenantService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<Tenant> tenantRepository,IScopeServiceLoader loader)
			:base(tenantRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.tenantRepository=tenantRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<TenantDto> QueryMain() 
		{
			var tenantQueryable=tenantRepository.Queryable;
			 var query = from _tenant in tenantQueryable 
					 select new TenantDto
					{
						FullName=_tenant.FullName,
						EnCode=_tenant.EnCode,
						UrlMark=_tenant.UrlMark,
						Parent_Id=_tenant.Parent_Id,
						CanHaveChildren=_tenant.CanHaveChildren,
						HandIcon_Id=_tenant.HandIcon_Id,
						Id=_tenant.Id,
					};
			return query;
		}
		#endregion
	}
}
