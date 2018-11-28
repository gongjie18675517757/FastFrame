namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using System.Linq; 
	/// <summary>
	///组织信息 服务类 
	/// </summary>
	public partial class TenantService:BaseService<Tenant, TenantDto>
	{
		#region 字段
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly TenantRepository tenantRepository;
		#endregion
		#region 构造函数
		public TenantService(ForeignRepository foreignRepository,UserRepository userRepository,TenantRepository tenantRepository,IScopeServiceLoader loader)
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
			 var query = from tenant in tenantQueryable 
					 select new TenantDto
					{
						Name=tenant.Name,
						EnCode=tenant.EnCode,
						Id=tenant.Id,
					};
			return query;
		}
		#endregion
	}
}
