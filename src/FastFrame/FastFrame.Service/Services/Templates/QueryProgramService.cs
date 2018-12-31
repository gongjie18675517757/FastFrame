namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///查询方案 服务类 
	/// </summary>
	public partial class QueryProgramService:BaseService<QueryProgram, QueryProgramDto>
	{
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<QueryProgram> queryProgramRepository;
		
		/*构造函数*/
		public QueryProgramService(IRepository<User> userRepository,IRepository<QueryProgram> queryProgramRepository,IScopeServiceLoader loader)
			:base(queryProgramRepository,loader)
		{
			this.userRepository=userRepository;
			this.queryProgramRepository=queryProgramRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<QueryProgramDto> QueryMain() 
		{
			var queryProgramQueryable=queryProgramRepository.Queryable;
			 var query = from _queryProgram in queryProgramQueryable 
					 select new QueryProgramDto
					{
						Name=_queryProgram.Name,
						ModuleName=_queryProgram.ModuleName,
						IsPublic=_queryProgram.IsPublic,
						User_Id=_queryProgram.User_Id,
						Id=_queryProgram.Id,
					};
			return query;
		}
		
	}
}
