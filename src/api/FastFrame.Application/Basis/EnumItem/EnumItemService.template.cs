namespace FastFrame.Application.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
		
	/// <summary>
	/// 数字字典 服务实现 
	/// </summary>
	public partial class EnumItemService:BaseService<EnumItem, EnumItemDto>
	{
		private readonly IRepository<EnumItem> enumItemRepository;
		private readonly IRepository<User> userRepository;
		
		public EnumItemService(IRepository<EnumItem> enumItemRepository,IRepository<User> userRepository)
			 : base(enumItemRepository)
		{
			this.enumItemRepository=enumItemRepository;
			this.userRepository=userRepository;
		}
		
		protected override IQueryable<EnumItemDto> QueryMain() 
		{
			var enumItemQueryable = enumItemRepository.Queryable.MapTo<EnumItem,EnumItemViewModel>();
			var userQueryable = userRepository.Queryable.MapTo<User,UserViewModel>();
			var query = from _enumItem in enumItemRepository 
						join _super_Id in enumItemQueryable on _enumItem.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _enumItem.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _enumItem.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new EnumItemDto
						{
							Key = _enumItem.Key,
							Value = _enumItem.Value,
							Super_Id = _enumItem.Super_Id,
							Id = _enumItem.Id,
							Create_User_Id = _enumItem.Create_User_Id,
							CreateTime = _enumItem.CreateTime,
							Modify_User_Id = _enumItem.Modify_User_Id,
							ModifyTime = _enumItem.ModifyTime,
							Super = _super_Id,
							Create_User = _create_User_Id,
							Modify_User = _modify_User_Id,
						};
			return query;
		}
		public Task<PageList<EnumItemViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = enumItemRepository.MapTo<EnumItem, EnumItemViewModel>();
			return query.PageListAsync(page);
		}
		
	}
}
