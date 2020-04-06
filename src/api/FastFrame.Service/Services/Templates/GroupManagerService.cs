namespace FastFrame.Service.Services.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Dto.Chat; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using FastFrame.Entity.Basis; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	/// <summary>
	///群组管理员 服务实现 
	/// </summary>
	public partial class GroupManagerService:BaseService<GroupManager, GroupManagerDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<GroupManager> groupManagerRepository;
		
		public GroupManagerService(IRepository<User> userRepository,IRepository<GroupManager> groupManagerRepository,IScopeServiceLoader loader)
			:base(groupManagerRepository,loader)
		{
			this.userRepository=userRepository;
			this.groupManagerRepository=groupManagerRepository;
		}
		
		
		protected override IQueryable<GroupManagerDto> QueryMain() 
		{
			var query = from _groupManager in groupManagerRepository 
						 select new GroupManagerDto
						{
							Group_Id=_groupManager.Group_Id,
							User_Id=_groupManager.User_Id,
							Id=_groupManager.Id,
					};
			return query;
		}
		public  Task<PageList<GroupManagerViewModel>> ViewModelListAsync(PagePara page) 
		{
			var query = from _groupManager in groupManagerRepository 
						select new GroupManagerViewModel
						{
							Group_Id = _groupManager.Group_Id,
							User_Id = _groupManager.User_Id,
							Id = _groupManager.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
