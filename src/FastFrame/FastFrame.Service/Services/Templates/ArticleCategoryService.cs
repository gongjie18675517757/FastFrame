namespace FastFrame.Service.Services.CMS
{
	using FastFrame.Repository.CMS; 
	using FastFrame.Entity.CMS; 
	using FastFrame.Dto.CMS; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository.Basis; 
	using System.Linq; 
	/// <summary>
	///文章类别 服务类 
	/// </summary>
	public partial class ArticleCategoryService:BaseService<ArticleCategory, ArticleCategoryDto>
	{
		#region 字段
		private readonly ArticleCategoryRepository articleCategoryRepository;
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		#endregion
		#region 构造函数
		public ArticleCategoryService(ArticleCategoryRepository articleCategoryRepository,ForeignRepository foreignRepository,UserRepository userRepository,IScopeServiceLoader loader)
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
			 var query = from articleCategory in articleCategoryQueryable 
						join parent_Id in articleCategoryQueryable.MapTo<ArticleCategory,ArticleCategoryDto>() on articleCategory.Parent_Id equals parent_Id.Id into t_parent_Id
						from parent_Id in t_parent_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on articleCategory.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new ArticleCategoryDto
					{
						Parent_Id=articleCategory.Parent_Id,
						Name=articleCategory.Name,
						Url=articleCategory.Url,
						Description=articleCategory.Description,
						Id=articleCategory.Id,
						Parent=parent_Id,
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
