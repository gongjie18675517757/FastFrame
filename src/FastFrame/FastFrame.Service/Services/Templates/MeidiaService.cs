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
	///图片库 服务类 
	/// </summary>
	public partial class MeidiaService:BaseService<Meidia, MeidiaDto>
	{
		private readonly IRepository<Meidia> meidiaRepository;
		private readonly IRepository<Resource> resourceRepository;
		private readonly IRepository<User> userRepository;
		
		public MeidiaService(IRepository<Meidia> meidiaRepository,IRepository<Resource> resourceRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(meidiaRepository,loader)
		{
			this.meidiaRepository=meidiaRepository;
			this.resourceRepository=resourceRepository;
			this.userRepository=userRepository;
		}
		
		
		protected override IQueryable<MeidiaDto> QueryMain() 
		{
			 var meidiaQueryable = meidiaRepository.Queryable;
			 var resourceQueryable = resourceRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _meidia in meidiaQueryable 
						join _parent_Id in meidiaQueryable on _meidia.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						join _resource_Id in resourceQueryable on _meidia.Resource_Id equals _resource_Id.Id into t__resource_Id
						from _resource_Id in t__resource_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _meidia.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _meidia.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						 select new MeidiaDto
						{
							Parent_Id=_meidia.Parent_Id,
							Href=_meidia.Href,
							Name=_meidia.Name,
							Resource_Id=_meidia.Resource_Id,
							ContentType=_meidia.ContentType,
							IsFolder=_meidia.IsFolder,
							Id=_meidia.Id,
							Create_User_Id=_meidia.Create_User_Id,
							CreateTime=_meidia.CreateTime,
							Modify_User_Id=_meidia.Modify_User_Id,
							ModifyTime=_meidia.ModifyTime,
							Parent=new MeidiaViewModel
							{
								Id = _parent_Id.Id,
								Name = _parent_Id.Name,
							},
							Resource=new ResourceViewModel
							{
								Id = _resource_Id.Id,
								Name = _resource_Id.Name,
								Size = _resource_Id.Size,
								Path = _resource_Id.Path,
								ContentType = _resource_Id.ContentType,
								MD5 = _resource_Id.MD5,
							},
							Create_User=new UserViewModel
							{
								Id = _create_User_Id.Id,
								Name = _create_User_Id.Name,
								Account = _create_User_Id.Account,
							},
							Modify_User=new UserViewModel
							{
								Id = _modify_User_Id.Id,
								Name = _modify_User_Id.Name,
								Account = _modify_User_Id.Account,
							},
					};
			return query;
		}
		
	}
}
