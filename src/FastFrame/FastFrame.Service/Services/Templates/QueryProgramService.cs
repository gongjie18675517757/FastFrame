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
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<QueryProgram> queryProgramRepository;
		#endregion
		#region 构造函数
		public QueryProgramService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<QueryProgram> queryProgramRepository,IScopeServiceLoader loader)
			:base(queryProgramRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.queryProgramRepository=queryProgramRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
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
		#endregion
	}
}
