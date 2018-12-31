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
	///文章类别 服务类 
	/// </summary>
	public partial class ArticleCategoryService:BaseService<ArticleCategory, ArticleCategoryDto>
	{
		/*字段*/
		private readonly IRepository<ArticleCategory> articleCategoryRepository;
		private readonly IRepository<User> userRepository;
		
		/*构造函数*/
		public ArticleCategoryService(IRepository<ArticleCategory> articleCategoryRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(articleCategoryRepository,loader)
		{
			this.articleCategoryRepository=articleCategoryRepository;
			this.userRepository=userRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<ArticleCategoryDto> QueryMain() 
		{
			 var articleCategoryQueryable = articleCategoryRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _articleCategory in articleCategoryQueryable 
						join _parent_Id in articleCategoryQueryable.MapTo<ArticleCategory,ArticleCategoryDto>() on _articleCategory.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _articleCategory.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _articleCategory.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
					 select new ArticleCategoryDto
					{
						Parent_Id=_articleCategory.Parent_Id,
						Name=_articleCategory.Name,
						Url=_articleCategory.Url,
						Description=_articleCategory.Description,
						Id=_articleCategory.Id,
						Create_User_Id=_articleCategory.Create_User_Id,
						CreateTime=_articleCategory.CreateTime,
						Modify_User_Id=_articleCategory.Modify_User_Id,
						ModifyTime=_articleCategory.ModifyTime,
						Parent=_parent_Id,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
