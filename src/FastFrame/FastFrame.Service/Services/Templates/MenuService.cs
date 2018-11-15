namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using System.Linq; 
	/// <summary>
	///菜单 服务类 
	/// </summary>
	public partial class MenuService:BaseService<Menu, MenuDto>
	{
		#region 字段
		private readonly MenuRepository menuRepository;
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		#endregion
		#region 构造函数
		public MenuService(MenuRepository menuRepository,ForeignRepository foreignRepository,UserRepository userRepository,IScopeServiceLoader loader)
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
			 var query = from menu in menuQueryable 
						join parent_Id in menuQueryable on menu.Parent_Id equals parent_Id.Id into t_parent_Id
						from parent_Id in t_parent_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on menu.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new MenuDto
					{
						EnCode=menu.EnCode,
						Name=menu.Name,
						Parent_Id=menu.Parent_Id,
						Title=menu.Title,
						Icon=menu.Icon,
						Path=menu.Path,
						Id=menu.Id,
						Parent_Name=parent_Id.Name,
						Parent_EnCode=parent_Id.EnCode,
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
