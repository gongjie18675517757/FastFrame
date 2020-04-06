namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	/// <summary>
	///图片库 服务实现 
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
			var query = from _meidia in meidiaRepository 
						join _super_Id in meidiaQueryable on _meidia.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _resource_Id in resourceQueryable on _meidia.Resource_Id equals _resource_Id.Id into t__resource_Id
						from _resource_Id in t__resource_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _meidia.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _meidia.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						let Super=new MeidiaViewModel {Name=_super_Id.Name,Id=_super_Id.Id}
						let Resource=new ResourceViewModel {Name=_resource_Id.Name,Id=_resource_Id.Id}
						let Create_User=new UserViewModel {Name=_create_User_Id.Name,Account=_create_User_Id.Account,Id=_create_User_Id.Id}
						let Modify_User=new UserViewModel {Name=_modify_User_Id.Name,Account=_modify_User_Id.Account,Id=_modify_User_Id.Id}
						 select new MeidiaDto
						{
							Super_Id=_meidia.Super_Id,
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
							Super=Super,
							Resource=Resource,
							Create_User=Create_User,
							Modify_User=Modify_User,
					};
			return query;
		}
		public  Task<PageList<MeidiaViewModel>> ViewModelListAsync(PagePara page) 
		{
			var query = from _meidia in meidiaRepository 
						select new MeidiaViewModel
						{
							Name = _meidia.Name,
							Id = _meidia.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
