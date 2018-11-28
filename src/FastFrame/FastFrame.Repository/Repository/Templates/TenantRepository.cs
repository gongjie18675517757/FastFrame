namespace FastFrame.Repository.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///组织信息[数据访问] 
	/// </summary>
	public partial class TenantRepository:BaseRepository<Tenant>,IRepository<Tenant>
	{
		#region 字段
		#endregion
		#region 构造函数
		public TenantRepository(DataBase context,ICurrentUserProvider currentUserProvider)
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
