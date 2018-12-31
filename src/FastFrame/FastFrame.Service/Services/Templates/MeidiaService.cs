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
		/*字段*/
		private readonly IRepository<Meidia> meidiaRepository;
		private readonly IRepository<Resource> resourceRepository;
		private readonly IRepository<User> userRepository;
		
		/*构造函数*/
		public MeidiaService(IRepository<Meidia> meidiaRepository,IRepository<Resource> resourceRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(meidiaRepository,loader)
		{
			this.meidiaRepository=meidiaRepository;
			this.resourceRepository=resourceRepository;
			this.userRepository=userRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<MeidiaDto> QueryMain() 
		{
			 var meidiaQueryable = meidiaRepository.Queryable;
			 var resourceQueryable = resourceRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _meidia in meidiaQueryable 
						join _parent_Id in meidiaQueryable.MapTo<Meidia,MeidiaDto>() on _meidia.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						join _resource_Id in resourceQueryable.MapTo<Resource,ResourceDto>() on _meidia.Resource_Id equals _resource_Id.Id into t__resource_Id
						from _resource_Id in t__resource_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _meidia.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _meidia.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
					 select new MeidiaDto
					{
						Parent_Id=_meidia.Parent_Id,
						Href=_meidia.Href,
						Name=_meidia.Name,
						Resource_Id=_meidia.Resource_Id,
						IsFolder=_meidia.IsFolder,
						Id=_meidia.Id,
						Create_User_Id=_meidia.Create_User_Id,
						CreateTime=_meidia.CreateTime,
						Modify_User_Id=_meidia.Modify_User_Id,
						ModifyTime=_meidia.ModifyTime,
						Parent=_parent_Id,
						Resource=_resource_Id,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
