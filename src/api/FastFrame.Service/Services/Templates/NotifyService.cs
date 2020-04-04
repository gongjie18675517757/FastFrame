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
	///通知 服务实现 
	/// </summary>
	public partial class NotifyService:BaseService<Notify, NotifyDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Resource> resourceRepository;
		private readonly IRepository<Notify> notifyRepository;
		
		public NotifyService(IRepository<User> userRepository,IRepository<Resource> resourceRepository,IRepository<Notify> notifyRepository,IScopeServiceLoader loader)
			:base(notifyRepository,loader)
		{
			this.userRepository=userRepository;
			this.resourceRepository=resourceRepository;
			this.notifyRepository=notifyRepository;
		}
		
		
		protected override IQueryable<NotifyDto> QueryMain() 
		{
			var userQueryable = userRepository.Queryable;
			var resourceQueryable = resourceRepository.Queryable;
			var query = from _notify in notifyRepository 
						join _publush_Id in userQueryable on _notify.Publush_Id equals _publush_Id.Id into t__publush_Id
						from _publush_Id in t__publush_Id.DefaultIfEmpty()
						join _resource_Id in resourceQueryable on _notify.Resource_Id equals _resource_Id.Id into t__resource_Id
						from _resource_Id in t__resource_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _notify.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _notify.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						let Publush=new UserViewModel {Name=_publush_Id.Name,Account=_publush_Id.Account,Id=_publush_Id.Id}
						let Resource=new ResourceViewModel {Name=_resource_Id.Name,Id=_resource_Id.Id}
						let Create_User=new UserViewModel {Name=_create_User_Id.Name,Account=_create_User_Id.Account,Id=_create_User_Id.Id}
						let Modify_User=new UserViewModel {Name=_modify_User_Id.Name,Account=_modify_User_Id.Account,Id=_modify_User_Id.Id}
						 select new NotifyDto
						{
							Title=_notify.Title,
							Type_Id=_notify.Type_Id,
							Publush_Id=_notify.Publush_Id,
							Resource_Id=_notify.Resource_Id,
							Content=_notify.Content,
							Id=_notify.Id,
							Create_User_Id=_notify.Create_User_Id,
							CreateTime=_notify.CreateTime,
							Modify_User_Id=_notify.Modify_User_Id,
							ModifyTime=_notify.ModifyTime,
							Publush=Publush,
							Resource=Resource,
							Create_User=Create_User,
							Modify_User=Modify_User,
					};
			return query;
		}
		protected  Task<PageList<NotifyViewModel>> ViewModelListAsync(PagePara page) 
		{
			var query = from _notify in notifyRepository 
						select new NotifyViewModel
						{
							Title = _notify.Title,
							Type_Id = _notify.Type_Id,
							Publush_Id = _notify.Publush_Id,
							Resource_Id = _notify.Resource_Id,
							Content = _notify.Content,
							Id = _notify.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
