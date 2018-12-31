namespace FastFrame.Service.Services.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.CMS; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///文章 服务类 
	/// </summary>
	public partial class ArticleService:BaseService<Article, ArticleDto>
	{
		/*字段*/
		private readonly IRepository<ArticleCategory> articleCategoryRepository;
		private readonly IRepository<Meidia> meidiaRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Article> articleRepository;
		
		/*构造函数*/
		public ArticleService(IRepository<ArticleCategory> articleCategoryRepository,IRepository<Meidia> meidiaRepository,IRepository<User> userRepository,IRepository<Article> articleRepository,IScopeServiceLoader loader)
			:base(articleRepository,loader)
		{
			this.articleCategoryRepository=articleCategoryRepository;
			this.meidiaRepository=meidiaRepository;
			this.userRepository=userRepository;
			this.articleRepository=articleRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<ArticleDto> QueryMain() 
		{
			var articleQueryable=articleRepository.Queryable;
			 var articleCategoryQueryable = articleCategoryRepository.Queryable;
			 var meidiaQueryable = meidiaRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _article in articleQueryable 
						join _articleCategory_Id in articleCategoryQueryable.MapTo<ArticleCategory,ArticleCategoryDto>() on _article.ArticleCategory_Id equals _articleCategory_Id.Id into t__articleCategory_Id
						from _articleCategory_Id in t__articleCategory_Id.DefaultIfEmpty()
						join _thumbnail_Id in meidiaQueryable.MapTo<Meidia,MeidiaDto>() on _article.Thumbnail_Id equals _thumbnail_Id.Id into t__thumbnail_Id
						from _thumbnail_Id in t__thumbnail_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _article.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _article.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
					 select new ArticleDto
					{
						Title=_article.Title,
						ArticleCategory_Id=_article.ArticleCategory_Id,
						Url=_article.Url,
						Summarize=_article.Summarize,
						Thumbnail_Id=_article.Thumbnail_Id,
						IsRelease=_article.IsRelease,
						LookCount=_article.LookCount,
						Description=_article.Description,
						ArticleContent_Id=_article.ArticleContent_Id,
						Id=_article.Id,
						Create_User_Id=_article.Create_User_Id,
						CreateTime=_article.CreateTime,
						Modify_User_Id=_article.Modify_User_Id,
						ModifyTime=_article.ModifyTime,
						ArticleCategory=_articleCategory_Id,
						Thumbnail=_thumbnail_Id,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
