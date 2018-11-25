namespace FastFrame.Repository.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Database; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///文章类别[数据访问] 
	/// </summary>
	public partial class ArticleCategoryRepository:BaseRepository<ArticleCategory>,IRepository<ArticleCategory>
	{
		#region 字段
		#endregion
		#region 构造函数
		public ArticleCategoryRepository(DataBase context,ICurrentUserProvider currentUserProvider)
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
