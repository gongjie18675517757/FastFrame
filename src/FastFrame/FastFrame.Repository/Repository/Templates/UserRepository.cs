namespace FastFrame.Repository.System
{
	using FastFrame.Entity.System; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///登陆用户[数据访问] 
	/// <summary>
	public partial class UserRepository:BaseRepository<User>,IRepository<User>
	{
		public UserRepository(DataBase context,ICurrentUserProvider currentUserProvider)
			:base(context,currentUserProvider)
		{
		}
	}
}
