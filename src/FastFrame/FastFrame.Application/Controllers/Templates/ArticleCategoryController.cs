namespace FastFrame.Application.Controllers.CMS
{
	using FastFrame.Dto.CMS; 
	using FastFrame.Entity.CMS; 
	using FastFrame.Service.Services.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///文章类别 
	/// </summary>
	[Permission(nameof(ArticleCategory),"文章类别")]
	public partial class ArticleCategoryController:BaseController<ArticleCategory, ArticleCategoryDto>
	{
		#region 字段
		private readonly ArticleCategoryService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public ArticleCategoryController(ArticleCategoryService service,IScopeServiceLoader serviceLoader)
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