namespace FastFrame.Repository.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///角色[数据访问] 
	/// </summary>
	public partial class RoleRepository:BaseRepository<Role>,IRepository<Role>
	{
		#region 字段
		#endregion
		#region 构造函数
		public RoleRepository(DataBase context,ICurrentUserProvider currentUserProvider)
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