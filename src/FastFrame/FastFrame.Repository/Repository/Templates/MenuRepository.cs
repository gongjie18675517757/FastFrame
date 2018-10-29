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
		#region 字段
		#endregion
		#region 构造函数
		public MenuRepository(DataBase context,ICurrentUserProvider currentUserProvider)
			:base(context,currentUserProvider)
		{
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		#endregion
	}
}
