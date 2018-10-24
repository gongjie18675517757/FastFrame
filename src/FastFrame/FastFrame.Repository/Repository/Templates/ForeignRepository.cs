namespace FastFrame.Repository.System
{
	using FastFrame.Entity.System; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///表外键信息[数据访问] 
	/// <summary>
	public partial class ForeignRepository:BaseRepository<Foreign>,IRepository<Foreign>
	{
		public ForeignRepository(DataBase context,ICurrentUserProvider currentUserProvider)
			:base(context,currentUserProvider)
		{
		}
	}
}
