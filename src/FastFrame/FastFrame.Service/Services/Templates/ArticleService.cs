namespace FastFrame.Service.Services.CMS
{
	using FastFrame.Repository.CMS; 
	using FastFrame.Entity.CMS; 
	using FastFrame.Dto.CMS; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using System.Linq;
    using FastFrame.Repository.Basis;

    /// <summary>
    ///文章 服务类 
    /// </summary>
    public partial class ArticleService:BaseService<Article, ArticleDto>
	{
		#region 字段
		private readonly ArticleCategoryRepository articleCategoryRepository;
		private readonly MeidiaRepository meidiaRepository;
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly ArticleRepository articleRepository;
		#endregion
		#region 构造函数
		public ArticleService(ArticleCategoryRepository articleCategoryRepository,MeidiaRepository meidiaRepository,ForeignRepository foreignRepository,UserRepository userRepository,ArticleRepository articleRepository,IScopeServiceLoader loader)
			:base(articleRepository,loader)
		{
			this.articleCategoryRepository=articleCategoryRepository;
			this.meidiaRepository=meidiaRepository;
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.articleRepository=articleRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<ArticleDto> QueryMain() 
		{
			var articleQueryable=articleRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var articleCategoryQueryable = articleCategoryRepository.Queryable;
			 var meidiaQueryable = meidiaRepository.Queryable;
			 var query = from article in articleQueryable 
						join articleCategory_Id in articleCategoryQueryable.MapTo<ArticleCategory,ArticleCategoryDto>() on article.ArticleCategory_Id equals articleCategory_Id.Id into t_articleCategory_Id
						from articleCategory_Id in t_articleCategory_Id.DefaultIfEmpty()
						join thumbnail_Id in meidiaQueryable.MapTo<Meidia,MeidiaDto>() on article.Thumbnail_Id equals thumbnail_Id.Id into t_thumbnail_Id
						from thumbnail_Id in t_thumbnail_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on article.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new ArticleDto
					{
						Title=article.Title,
						ArticleCategory_Id=article.ArticleCategory_Id,
						Url=article.Url,
						Summarize=article.Summarize,
						Thumbnail_Id=article.Thumbnail_Id,
						Content=article.Content,
						IsRelease=article.IsRelease,
						LookCount=article.LookCount,
						Description=article.Description,
						Id=article.Id,
						ArticleCategory=articleCategory_Id,
						Thumbnail=thumbnail_Id,
						CreateAccount = user2.Account,
						CreateName = user2.Name,
						CreateTime = foreing.CreateTime,
						ModifyAccount = user3.Account,
						ModifyName = user3.Name,
						ModifyTime = foreing.ModifyTime,
					};
			return query;
		}
		#endregion
	}
}
