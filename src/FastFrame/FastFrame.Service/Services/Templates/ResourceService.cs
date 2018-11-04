namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using System.Linq; 
	/// <summary>
	///资源 服务类 
	/// <summary>
	public partial class ResourceService:BaseService<Resource, ResourceDto>
	{
		#region 字段
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly ResourceRepository resourceRepository;
		#endregion
		#region 构造函数
		public ResourceService(ForeignRepository foreignRepository,UserRepository userRepository,ResourceRepository resourceRepository,IScopeServiceLoader loader)
			:base(resourceRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.resourceRepository=resourceRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<ResourceDto> QueryMain() 
		{
			var resourceQueryable=resourceRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var query = from resource in resourceQueryable 
					join foreing in foreignQueryable on resource.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new ResourceDto
					{
						Name=resource.Name,
						Size=resource.Size,
						Path=resource.Path,
						ContentType=resource.ContentType,
						Id=resource.Id,
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
