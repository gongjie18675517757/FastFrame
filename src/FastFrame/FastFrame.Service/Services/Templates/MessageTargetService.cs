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
	/// <summary>
	///消息接收人 服务类 
	/// </summary>
	public partial class MessageTargetService:BaseService<MessageTarget, MessageTargetDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<MessageTarget> messageTargetRepository;
		
		public MessageTargetService(IRepository<User> userRepository,IRepository<MessageTarget> messageTargetRepository,IScopeServiceLoader loader)
			:base(messageTargetRepository,loader)
		{
			this.userRepository=userRepository;
			this.messageTargetRepository=messageTargetRepository;
		}
		
		
		protected override IQueryable<MessageTargetDto> QueryMain() 
		{
			var messageTargetQueryable=messageTargetRepository.Queryable;
			 var query = from _messageTarget in messageTargetQueryable 
						 select new MessageTargetDto
						{
							Message_Id=_messageTarget.Message_Id,
							To_Id=_messageTarget.To_Id,
							HaveRead=_messageTarget.HaveRead,
							Id=_messageTarget.Id,
					};
			return query;
		}
		
	}
}
