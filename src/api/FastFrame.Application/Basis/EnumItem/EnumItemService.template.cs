	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 数字字典 服务实现 
	/// </summary>
	public partial class EnumItemService:BaseService<EnumItem, EnumItemDto>
	{
		private readonly IRepository<EnumItem> enumItemRepository;
		private readonly IRepository<User> userRepository;
		
		public EnumItemService(IRepository<EnumItem> enumItemRepository,IRepository<User> userRepository,IServiceProvider loader)
			 : base(loader,enumItemRepository)
		{
			this.enumItemRepository=enumItemRepository;
			this.userRepository=userRepository;
		}
		
		protected override IQueryable<EnumItemDto> DefaultQueryable() 
		{
			var enumItemQueryable = enumItemRepository.Queryable.Select(EnumItem.BuildExpression());
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var repository = enumItemRepository.Queryable;
			var query = from _enumItem in repository 
						join _super_Id in enumItemQueryable on _enumItem.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _enumItem.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _enumItem.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new EnumItemDto
						{
							Key = _enumItem.Key,
							Super_Id = _enumItem.Super_Id,
							Value = _enumItem.Value,
							IntKey = _enumItem.IntKey,
							SortVal = _enumItem.SortVal,
							Id = _enumItem.Id,
							Create_User_Id = _enumItem.Create_User_Id,
							CreateTime = _enumItem.CreateTime,
							Modify_User_Id = _enumItem.Modify_User_Id,
							ModifyTime = _enumItem.ModifyTime,
							Super_Value = _super_Id.Value,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		protected override IQueryable<Entity.IViewModel> DefaultViewModelQueryable() 
		{
			return repository.Select(EnumItem.BuildExpression());
		}
		protected override IQueryable<ITreeModel> DefaultTreeModelQueryable() 
		{
			return from a in repository
					join b in repository.Select(EnumItem.BuildExpression()) on a.Id equals b.Id
					select new TreeModel
					{
					Id = a.Id,
					Super_Id = a.Super_Id,
					Value = b.Value,
					ChildCount = repository.Count(v => v.Super_Id == a.Id),
					TotalChildCount = repository.Count(v => v.Id != a.Id && v.TreeCode.StartsWith(a.TreeCode)),
				};
		}
		
	}
}
