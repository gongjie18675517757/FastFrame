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
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<GroupMessage> groupMessageRepository;
		
		/*构造函数*/
		public GroupMessageService(IRepository<User> userRepository,IRepository<GroupMessage> groupMessageRepository,IScopeServiceLoader loader)
			:base(groupMessageRepository,loader)
		{
			this.userRepository=userRepository;
			this.groupMessageRepository=groupMessageRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<GroupMessageDto> QueryMain() 
		{
			var groupMessageQueryable=groupMessageRepository.Queryable;
			 var query = from _groupMessage in groupMessageQueryable 
					 select new GroupMessageDto
					{
						Group_Id=_groupMessage.Group_Id,
						Content=_groupMessage.Content,
						Category=_groupMessage.Category,
						Resource_Id=_groupMessage.Resource_Id,
						From_Id=_groupMessage.From_Id,
						MessageTime=_groupMessage.MessageTime,
						Id=_groupMessage.Id,
					};
			return query;
		}
		
	}
}
