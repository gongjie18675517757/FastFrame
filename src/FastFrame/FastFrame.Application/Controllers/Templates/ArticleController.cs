namespace FastFrame.Application.Controllers.CMS
{
	using FastFrame.Dto.CMS; 
	using FastFrame.Entity.CMS; 
	using FastFrame.Service.Services.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///文章 
	/// </summary>
	[Permission(nameof(Article),"文章")]
	public partial class ArticleController:BaseController<Article, ArticleDto>
	{
		#region 字段
		private readonly ArticleService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public ArticleController(ArticleService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		#endregion
	}
}
