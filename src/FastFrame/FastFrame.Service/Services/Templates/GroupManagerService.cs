namespace FastFrame.Service.Services.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Dto.Chat; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using FastFrame.Entity.Basis; 
	using System.Linq; 
	/// <summary>
	///群组管理员 服务类 
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
			var groupManagerQueryable=groupManagerRepository.Queryable;
			 var query = from _groupManager in groupManagerQueryable 
					 select new GroupManagerDto
					{
						Group_Id=_groupManager.Group_Id,
						User_Id=_groupManager.User_Id,
						Id=_groupManager.Id,
					};
			return query;
		}
		
	}
}
