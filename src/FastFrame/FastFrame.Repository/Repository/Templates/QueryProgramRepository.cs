namespace FastFrame.Repository.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///查询方案[数据访问] 
	/// <summary>
	public partial class QueryProgramRepository:BaseRepository<QueryProgram>,IRepository<QueryProgram>
	{
		public QueryProgramRepository(DataBase context,ICurrentUserProvider currentUserProvider)
			:base(context,currentUserProvider)
		{
		}
	}
}
