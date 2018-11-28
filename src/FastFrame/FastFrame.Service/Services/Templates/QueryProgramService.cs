namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using System.Linq; 
	/// <summary>
	///查询方案 服务类 
	/// </summary>
	public partial class QueryProgramService:BaseService<QueryProgram, QueryProgramDto>
	{
		#region 字段
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly QueryProgramRepository queryProgramRepository;
		#endregion
		#region 构造函数
		public QueryProgramService(ForeignRepository foreignRepository,UserRepository userRepository,QueryProgramRepository queryProgramRepository,IScopeServiceLoader loader)
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
			 var query = from queryProgram in queryProgramQueryable 
					 select new QueryProgramDto
					{
						Name=queryProgram.Name,
						ModuleName=queryProgram.ModuleName,
						IsPublic=queryProgram.IsPublic,
						User_Id=queryProgram.User_Id,
						Id=queryProgram.Id,
					};
			return query;
		}
		#endregion
	}
}
