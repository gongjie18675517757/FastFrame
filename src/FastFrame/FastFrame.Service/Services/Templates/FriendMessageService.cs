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
	///好友消息 服务类 
	/// </summary>
	public partial class FriendMessageService:BaseService<FriendMessage, FriendMessageDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<FriendMessage> friendMessageRepository;
		#endregion
		#region 构造函数
		public FriendMessageService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<FriendMessage> friendMessageRepository,IScopeServiceLoader loader)
			:base(friendMessageRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.friendMessageRepository=friendMessageRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<FriendMessageDto> QueryMain() 
		{
			var friendMessageQueryable=friendMessageRepository.Queryable;
			 var query = from _friendMessage in friendMessageQueryable 
					 select new FriendMessageDto
					{
						Content=_friendMessage.Content,
						Category=_friendMessage.Category,
						Resource_Id=_friendMessage.Resource_Id,
						From_Id=_friendMessage.From_Id,
						MessageTime=_friendMessage.MessageTime,
						Id=_friendMessage.Id,
					};
			return query;
		}
		#endregion
	}
}
