namespace FastFrame.Repository.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///登陆用户[数据访问] 
	/// </summary>
	public partial class UserRepository:BaseRepository<User>,IRepository<User>
	{
		#region 字段
		#endregion
		#region 构造函数
		public UserRepository(DataBase context,ICurrentUserProvider currentUserProvider)
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
