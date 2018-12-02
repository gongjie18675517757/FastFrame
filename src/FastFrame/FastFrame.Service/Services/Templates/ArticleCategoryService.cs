namespace FastFrame.Service.Services.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Dto.CMS; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using FastFrame.Entity.Basis; 
	using System.Linq; 
	/// <summary>
	///文章类别 服务类 
	/// </summary>
	public partial class ArticleCategoryService:BaseService<ArticleCategory, ArticleCategoryDto>
	{
		#region 字段
		private readonly IRepository<ArticleCategory> articleCategoryRepository;
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		#endregion
		#region 构造函数
		public ArticleCategoryService(IRepository<ArticleCategory> articleCategoryRepository,IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(articleCategoryRepository,loader)
		{
			this.articleCategoryRepository=articleCategoryRepository;
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<ArticleCategoryDto> QueryMain() 
		{
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var articleCategoryQueryable = articleCategoryRepository.Queryable;
			 var query = from _articleCategory in articleCategoryQueryable 
						join _parent_Id in articleCategoryQueryable.MapTo<ArticleCategory,ArticleCategoryDto>() on _articleCategory.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on _articleCategory.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new ArticleCategoryDto
					{
						Parent_Id=_articleCategory.Parent_Id,
						Name=_articleCategory.Name,
						Url=_articleCategory.Url,
						Description=_articleCategory.Description,
						Id=_articleCategory.Id,
						Parent=_parent_Id,
						Foreign = foreing,
						Create_User = user2,
						Modify_User = user3,
					};
			return query;
		}
		#endregion
	}
}
