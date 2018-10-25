namespace FastFrame.Repository.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///组织信息[数据访问] 
	/// <summary>
	public partial class OrganizeRepository:BaseRepository<Organize>,IRepository<Organize>
	{
		public OrganizeRepository(DataBase context,ICurrentUserProvider currentUserProvider)
			:base(context,currentUserProvider)
		{
		}
	}
}
