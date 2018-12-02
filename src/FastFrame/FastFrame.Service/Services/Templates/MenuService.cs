namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///菜单 服务类 
	/// </summary>
	public partial class MenuService:BaseService<Menu, MenuDto>
	{
		#region 字段
		private readonly IRepository<Menu> menuRepository;
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		#endregion
		#region 构造函数
		public MenuService(IRepository<Menu> menuRepository,IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(menuRepository,loader)
		{
			this.menuRepository=menuRepository;
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<MenuDto> QueryMain() 
		{
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var menuQueryable = menuRepository.Queryable;
			 var query = from _menu in menuQueryable 
						join _parent_Id in menuQueryable.MapTo<Menu,MenuDto>() on _menu.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on _menu.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new MenuDto
					{
						EnCode=_menu.EnCode,
						Name=_menu.Name,
						Parent_Id=_menu.Parent_Id,
						Title=_menu.Title,
						Icon=_menu.Icon,
						Path=_menu.Path,
						Id=_menu.Id,
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
