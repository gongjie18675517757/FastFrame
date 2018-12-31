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
		/*字段*/
		private readonly IRepository<Menu> menuRepository;
		private readonly IRepository<User> userRepository;
		
		/*构造函数*/
		public MenuService(IRepository<Menu> menuRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(menuRepository,loader)
		{
			this.menuRepository=menuRepository;
			this.userRepository=userRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<MenuDto> QueryMain() 
		{
			 var menuQueryable = menuRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _menu in menuQueryable 
						join _parent_Id in menuQueryable.MapTo<Menu,MenuDto>() on _menu.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _menu.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _menu.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
					 select new MenuDto
					{
						EnCode=_menu.EnCode,
						Name=_menu.Name,
						Parent_Id=_menu.Parent_Id,
						Title=_menu.Title,
						Icon=_menu.Icon,
						Path=_menu.Path,
						Id=_menu.Id,
						Create_User_Id=_menu.Create_User_Id,
						CreateTime=_menu.CreateTime,
						Modify_User_Id=_menu.Modify_User_Id,
						ModifyTime=_menu.ModifyTime,
						Parent=_parent_Id,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
