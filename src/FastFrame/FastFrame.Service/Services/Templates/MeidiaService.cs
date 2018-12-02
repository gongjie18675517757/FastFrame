namespace FastFrame.Service.Services.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.CMS; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///图片库 服务类 
	/// </summary>
	public partial class MeidiaService:BaseService<Meidia, MeidiaDto>
	{
		#region 字段
		private readonly IRepository<Meidia> meidiaRepository;
		private readonly IRepository<Resource> resourceRepository;
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		#endregion
		#region 构造函数
		public MeidiaService(IRepository<Meidia> meidiaRepository,IRepository<Resource> resourceRepository,IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(meidiaRepository,loader)
		{
			this.meidiaRepository=meidiaRepository;
			this.resourceRepository=resourceRepository;
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<MeidiaDto> QueryMain() 
		{
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var meidiaQueryable = meidiaRepository.Queryable;
			 var resourceQueryable = resourceRepository.Queryable;
			 var query = from _meidia in meidiaQueryable 
						join _parent_Id in meidiaQueryable.MapTo<Meidia,MeidiaDto>() on _meidia.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						join _resource_Id in resourceQueryable.MapTo<Resource,ResourceDto>() on _meidia.Resource_Id equals _resource_Id.Id into t__resource_Id
						from _resource_Id in t__resource_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on _meidia.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new MeidiaDto
					{
						Parent_Id=_meidia.Parent_Id,
						Href=_meidia.Href,
						Name=_meidia.Name,
						Resource_Id=_meidia.Resource_Id,
						IsFolder=_meidia.IsFolder,
						Id=_meidia.Id,
						Parent=_parent_Id,
						Resource=_resource_Id,
						Foreign = foreing,
						Create_User = user2,
						Modify_User = user3,
					};
			return query;
		}
		#endregion
	}
}
