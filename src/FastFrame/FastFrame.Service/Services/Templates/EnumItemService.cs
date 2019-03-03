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
	/// 服务类 
	/// </summary>
	public partial class EnumItemService:BaseService<EnumItem, EnumItemDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EnumItem> enumItemRepository;
		
		public EnumItemService(IRepository<User> userRepository,IRepository<EnumItem> enumItemRepository,IScopeServiceLoader loader)
			:base(enumItemRepository,loader)
		{
			this.userRepository=userRepository;
			this.enumItemRepository=enumItemRepository;
		}
		
		
		protected override IQueryable<EnumItemDto> QueryMain() 
		{
			var enumItemQueryable=enumItemRepository.Queryable;
			 var query = from _enumItem in enumItemQueryable 
						 select new EnumItemDto
						{
							EnumName=_enumItem.EnumName,
							EnumValue=_enumItem.EnumValue,
							Parent_Id=_enumItem.Parent_Id,
							Id=_enumItem.Id,
					};
			return query;
		}
		
	}
}
