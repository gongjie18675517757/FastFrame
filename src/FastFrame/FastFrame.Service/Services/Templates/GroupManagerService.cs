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
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<GroupManager> groupManagerRepository;
		#endregion
		#region 构造函数
		public GroupManagerService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<GroupManager> groupManagerRepository,IScopeServiceLoader loader)
			:base(groupManagerRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.groupManagerRepository=groupManagerRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
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
		#endregion
	}
}