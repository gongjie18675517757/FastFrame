namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///查询方案明细 服务类 
	/// </summary>
	public partial class QueryProgramDetailService:BaseService<QueryProgramDetail, QueryProgramDetailDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<QueryProgramDetail> queryProgramDetailRepository;
		#endregion
		#region 构造函数
		public QueryProgramDetailService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<QueryProgramDetail> queryProgramDetailRepository,IScopeServiceLoader loader)
			:base(queryProgramDetailRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.queryProgramDetailRepository=queryProgramDetailRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<QueryProgramDetailDto> QueryMain() 
		{
			var queryProgramDetailQueryable=queryProgramDetailRepository.Queryable;
			 var query = from queryProgramDetail in queryProgramDetailQueryable 
					 select new QueryProgramDetailDto
					{
						QueryProgram_Id=queryProgramDetail.QueryProgram_Id,
						Name=queryProgramDetail.Name,
						Compare=queryProgramDetail.Compare,
						Value=queryProgramDetail.Value,
						Id=queryProgramDetail.Id,
					};
			return query;
		}
		#endregion
	}
}
