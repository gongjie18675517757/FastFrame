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
	///群组消息 服务实现 
	/// </summary>
	public partial class GroupMessageService:BaseService<GroupMessage, GroupMessageDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<GroupMessage> groupMessageRepository;
		
		public GroupMessageService(IRepository<User> userRepository,IRepository<GroupMessage> groupMessageRepository)
			:base(groupMessageRepository)
		{
			this.userRepository=userRepository;
			this.groupMessageRepository=groupMessageRepository;
		}
		
		
		protected override IQueryable<GroupMessageDto> QueryMain() 
		{
			var query = from _groupMessage in groupMessageRepository 
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
		public  Task<PageList<GroupMessageViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = from _groupMessage in groupMessageRepository 
						select new GroupMessageViewModel
						{
							Group_Id = _groupMessage.Group_Id,
							Id = _groupMessage.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
