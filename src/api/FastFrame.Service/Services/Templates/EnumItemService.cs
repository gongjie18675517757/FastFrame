namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	/// <summary>
	///数字字典 服务实现 
	/// </summary>
	public partial class EnumItemService:BaseService<EnumItem, EnumItemDto>
	{
		private readonly IRepository<EnumItem> enumItemRepository;
		private readonly IRepository<User> userRepository;
		
		public EnumItemService(IRepository<EnumItem> enumItemRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(enumItemRepository,loader)
		{
			this.enumItemRepository=enumItemRepository;
			this.userRepository=userRepository;
		}
		
		
		protected override IQueryable<EnumItemDto> QueryMain() 
		{
			var enumItemQueryable = enumItemRepository.Queryable;
			var userQueryable = userRepository.Queryable;
			var query = from _enumItem in enumItemRepository 
						join _super_Id in enumItemQueryable on _enumItem.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _enumItem.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _enumItem.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						let Super=new EnumItemViewModel {Value=_super_Id.Value,Id=_super_Id.Id}
						let Create_User=new UserViewModel {Name=_create_User_Id.Name,Account=_create_User_Id.Account,Id=_create_User_Id.Id}
						let Modify_User=new UserViewModel {Name=_modify_User_Id.Name,Account=_modify_User_Id.Account,Id=_modify_User_Id.Id}
						 select new EnumItemDto
						{
							Key=_enumItem.Key,
							Value=_enumItem.Value,
							Super_Id=_enumItem.Super_Id,
							Id=_enumItem.Id,
							Create_User_Id=_enumItem.Create_User_Id,
							CreateTime=_enumItem.CreateTime,
							Modify_User_Id=_enumItem.Modify_User_Id,
							ModifyTime=_enumItem.ModifyTime,
							Super=Super,
							Create_User=Create_User,
							Modify_User=Modify_User,
					};
			return query;
		}
		protected  Task<PageList<EnumItemViewModel>> ViewModelListAsync(PagePara page) 
		{
			var query = from _enumItem in enumItemRepository 
						select new EnumItemViewModel
						{
							Value = _enumItem.Value,
							Id = _enumItem.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
