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
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<QueryProgramDetail> queryProgramDetailRepository;
		
		/*构造函数*/
		public QueryProgramDetailService(IRepository<User> userRepository,IRepository<QueryProgramDetail> queryProgramDetailRepository,IScopeServiceLoader loader)
			:base(queryProgramDetailRepository,loader)
		{
			this.userRepository=userRepository;
			this.queryProgramDetailRepository=queryProgramDetailRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<QueryProgramDetailDto> QueryMain() 
		{
			var queryProgramDetailQueryable=queryProgramDetailRepository.Queryable;
			 var query = from _queryProgramDetail in queryProgramDetailQueryable 
					 select new QueryProgramDetailDto
					{
						QueryProgram_Id=_queryProgramDetail.QueryProgram_Id,
						Name=_queryProgramDetail.Name,
						Compare=_queryProgramDetail.Compare,
						Value=_queryProgramDetail.Value,
						Id=_queryProgramDetail.Id,
					};
			return query;
		}
		
	}
}
