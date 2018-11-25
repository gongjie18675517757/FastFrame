namespace FastFrame.Repository.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///图片库[数据访问] 
	/// </summary>
	public partial class MeidiaRepository:BaseRepository<Meidia>,IRepository<Meidia>
	{
		#region 字段
		#endregion
		#region 构造函数
		public MeidiaRepository(DataBase context,ICurrentUserProvider currentUserProvider)
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
