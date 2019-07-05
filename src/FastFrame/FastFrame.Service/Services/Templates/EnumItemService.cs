namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	/// <summary>
	///数字字典 服务类 
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
			 var query = from _enumItem in enumItemQueryable 
						join _super_Id in enumItemQueryable.TagWith("_super_Id") on _enumItem.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.TagWith("_create_User_Id") on _enumItem.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.TagWith("_modify_User_Id") on _enumItem.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
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
							Super=_super_Id==null?null:new EnumItemDto
							{
								Id = _super_Id.Id,
								Key = _super_Id.Key,
								Value = _super_Id.Value,
								Super_Id = _super_Id.Super_Id,
								Super = null,
								Create_User_Id = _super_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _super_Id.CreateTime,
								Modify_User_Id = _super_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _super_Id.ModifyTime,
							},
							Create_User=_create_User_Id==null?null:new UserDto
							{
								Id = _create_User_Id.Id,
								Account = _create_User_Id.Account,
								Password = _create_User_Id.Password,
								Name = _create_User_Id.Name,
								Email = _create_User_Id.Email,
								PhoneNumber = _create_User_Id.PhoneNumber,
								HandIcon_Id = _create_User_Id.HandIcon_Id,
								HandIcon = null,
								IsAdmin = _create_User_Id.IsAdmin,
								IsDisabled = _create_User_Id.IsDisabled,
								Create_User_Id = _create_User_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _create_User_Id.CreateTime,
								Modify_User_Id = _create_User_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _create_User_Id.ModifyTime,
							},
							Modify_User=_modify_User_Id==null?null:new UserDto
							{
								Id = _modify_User_Id.Id,
								Account = _modify_User_Id.Account,
								Password = _modify_User_Id.Password,
								Name = _modify_User_Id.Name,
								Email = _modify_User_Id.Email,
								PhoneNumber = _modify_User_Id.PhoneNumber,
								HandIcon_Id = _modify_User_Id.HandIcon_Id,
								HandIcon = null,
								IsAdmin = _modify_User_Id.IsAdmin,
								IsDisabled = _modify_User_Id.IsDisabled,
								Create_User_Id = _modify_User_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _modify_User_Id.CreateTime,
								Modify_User_Id = _modify_User_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _modify_User_Id.ModifyTime,
							},
					};
			return query;
		}
		
	}
}
