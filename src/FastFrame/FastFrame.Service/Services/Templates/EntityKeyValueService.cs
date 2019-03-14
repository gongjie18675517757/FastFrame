namespace FastFrame.Service.Services.Module
{
	using FastFrame.Entity.Module; 
	using FastFrame.Dto.Module; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using FastFrame.Entity.Basis; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	/// <summary>
	///键值对 服务类 
	/// </summary>
	public partial class EntityKeyValueService:BaseService<EntityKeyValue, EntityKeyValueDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EntityKeyValue> entityKeyValueRepository;
		
		public EntityKeyValueService(IRepository<User> userRepository,IRepository<EntityKeyValue> entityKeyValueRepository,IScopeServiceLoader loader)
			:base(entityKeyValueRepository,loader)
		{
			this.userRepository=userRepository;
			this.entityKeyValueRepository=entityKeyValueRepository;
		}
		
		
		protected override IQueryable<EntityKeyValueDto> QueryMain() 
		{
			var entityKeyValueQueryable=entityKeyValueRepository.Queryable;
			 var query = from _entityKeyValue in entityKeyValueQueryable 
						 select new EntityKeyValueDto
						{
							Id=_entityKeyValue.Id,
							Source_Id=_entityKeyValue.Source_Id,
							Key=_entityKeyValue.Key,
							Value=_entityKeyValue.Value,
					};
			return query;
		}
		
	}
}
