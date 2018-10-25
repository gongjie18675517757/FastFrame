namespace FastFrame.Repository.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///菜单[数据访问] 
	/// <summary>
	public partial class MenuRepository:BaseRepository<Menu>,IRepository<Menu>
	{
		public MenuRepository(DataBase context,ICurrentUserProvider currentUserProvider)
			:base(context,currentUserProvider)
		{
		}
	}
}
