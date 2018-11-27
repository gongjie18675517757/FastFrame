namespace FastFrame.Repository.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///文章内容[数据访问] 
	/// </summary>
	public partial class ArticleContentRepository:BaseRepository<ArticleContent>,IRepository<ArticleContent>
	{
		#region 字段
		#endregion
		#region 构造函数
		public ArticleContentRepository(DataBase context,ICurrentUserProvider currentUserProvider)
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
