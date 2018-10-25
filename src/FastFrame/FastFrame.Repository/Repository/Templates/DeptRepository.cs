namespace FastFrame.Repository.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///部门[数据访问] 
	/// <summary>
	public partial class DeptRepository:BaseRepository<Dept>,IRepository<Dept>
	{
		public DeptRepository(DataBase context,ICurrentUserProvider currentUserProvider)
			:base(context,currentUserProvider)
		{
		}
	}
}
