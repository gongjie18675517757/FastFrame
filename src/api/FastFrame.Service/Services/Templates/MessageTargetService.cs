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
	///消息接收人 服务实现 
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
			var query = from _messageTarget in messageTargetRepository 
						 select new MessageTargetDto
						{
							Message_Id=_messageTarget.Message_Id,
							To_Id=_messageTarget.To_Id,
							HaveRead=_messageTarget.HaveRead,
							Id=_messageTarget.Id,
					};
			return query;
		}
		public  Task<PageList<MessageTargetViewModel>> ViewModelListAsync(PagePara page) 
		{
			var query = from _messageTarget in messageTargetRepository 
						select new MessageTargetViewModel
						{
							Message_Id = _messageTarget.Message_Id,
							Id = _messageTarget.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
