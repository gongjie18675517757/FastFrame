namespace FastFrame.Application.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
		
	/// <summary>
	/// 资源 服务实现 
	/// </summary>
	public partial class ResourceService:BaseService<Resource, ResourceDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Resource> resourceRepository;
		
		public ResourceService(IRepository<User> userRepository,IRepository<Resource> resourceRepository)
			 : base(resourceRepository)
		{
			this.userRepository=userRepository;
			this.resourceRepository=resourceRepository;
		}
		
		protected override IQueryable<ResourceDto> QueryMain() 
		{
			var repository = resourceRepository.Queryable;
			var query = from _resource in repository 
						select new ResourceDto
						{
							Name = _resource.Name,
							Size = _resource.Size,
							Path = _resource.Path,
							ContentType = _resource.ContentType,
							MD5 = _resource.MD5,
							Uploader_Id = _resource.Uploader_Id,
							UploadTime = _resource.UploadTime,
							Id = _resource.Id,
						};
			return query;
		}
		public Task<PageList<ResourceViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = resourceRepository.MapTo<Resource, ResourceViewModel>();
			return query.PageListAsync(page);
		}
		
	}
}