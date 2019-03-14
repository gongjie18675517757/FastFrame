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
	///资源 服务类 
	/// </summary>
	public partial class ResourceService:BaseService<Resource, ResourceDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Resource> resourceRepository;
		
		public ResourceService(IRepository<User> userRepository,IRepository<Resource> resourceRepository,IScopeServiceLoader loader)
			:base(resourceRepository,loader)
		{
			this.userRepository=userRepository;
			this.resourceRepository=resourceRepository;
		}
		
		
		protected override IQueryable<ResourceDto> QueryMain() 
		{
			var resourceQueryable=resourceRepository.Queryable;
			 var query = from _resource in resourceQueryable 
						 select new ResourceDto
						{
							Name=_resource.Name,
							Size=_resource.Size,
							Path=_resource.Path,
							ContentType=_resource.ContentType,
							MD5=_resource.MD5,
							Id=_resource.Id,
					};
			return query;
		}
		
	}
}
