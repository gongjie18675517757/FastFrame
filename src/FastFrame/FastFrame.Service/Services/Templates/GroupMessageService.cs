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
	///群组消息 服务类 
	/// </summary>
	public partial class GroupMessageService:BaseService<GroupMessage, GroupMessageDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<GroupMessage> groupMessageRepository;
		#endregion
		#region 构造函数
		public GroupMessageService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<GroupMessage> groupMessageRepository,IScopeServiceLoader loader)
			:base(groupMessageRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.groupMessageRepository=groupMessageRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<GroupMessageDto> QueryMain() 
		{
			var groupMessageQueryable=groupMessageRepository.Queryable;
			 var query = from groupMessage in groupMessageQueryable 
					 select new GroupMessageDto
					{
						Group_Id=groupMessage.Group_Id,
						Content=groupMessage.Content,
						Category=groupMessage.Category,
						Resource_Id=groupMessage.Resource_Id,
						From_Id=groupMessage.From_Id,
						MessageTime=groupMessage.MessageTime,
						Id=groupMessage.Id,
						Tenant_Id=groupMessage.Tenant_Id,
					};
			return query;
		}
		#endregion
	}
}
