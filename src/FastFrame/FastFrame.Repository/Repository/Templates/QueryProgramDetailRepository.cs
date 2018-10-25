namespace FastFrame.Repository.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///查询方案明细[数据访问] 
	/// <summary>
	public partial class QueryProgramDetailRepository:BaseRepository<QueryProgramDetail>,IRepository<QueryProgramDetail>
	{
		public QueryProgramDetailRepository(DataBase context,ICurrentUserProvider currentUserProvider)
			:base(context,currentUserProvider)
		{
		}
	}
}
