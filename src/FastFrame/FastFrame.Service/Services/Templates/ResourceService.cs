namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///资源 服务类 
	/// </summary>
	public partial class ResourceService:BaseService<Resource, ResourceDto>
	{
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Resource> resourceRepository;
		
		/*构造函数*/
		public ResourceService(IRepository<User> userRepository,IRepository<Resource> resourceRepository,IScopeServiceLoader loader)
			:base(resourceRepository,loader)
		{
			this.userRepository=userRepository;
			this.resourceRepository=resourceRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<ResourceDto> QueryMain() 
		{
			var resourceQueryable=resourceRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _resource in resourceQueryable 
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _resource.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _resource.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
					 select new ResourceDto
					{
						Name=_resource.Name,
						Size=_resource.Size,
						Path=_resource.Path,
						ContentType=_resource.ContentType,
						Id=_resource.Id,
						Create_User_Id=_resource.Create_User_Id,
						CreateTime=_resource.CreateTime,
						Modify_User_Id=_resource.Modify_User_Id,
						ModifyTime=_resource.ModifyTime,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
