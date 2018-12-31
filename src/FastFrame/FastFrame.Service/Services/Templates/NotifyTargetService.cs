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
	///通知目标 服务类 
	/// </summary>
	public partial class NotifyTargetService:BaseService<NotifyTarget, NotifyTargetDto>
	{
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<NotifyTarget> notifyTargetRepository;
		
		/*构造函数*/
		public NotifyTargetService(IRepository<User> userRepository,IRepository<NotifyTarget> notifyTargetRepository,IScopeServiceLoader loader)
			:base(notifyTargetRepository,loader)
		{
			this.userRepository=userRepository;
			this.notifyTargetRepository=notifyTargetRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<NotifyTargetDto> QueryMain() 
		{
			var notifyTargetQueryable=notifyTargetRepository.Queryable;
			 var query = from _notifyTarget in notifyTargetQueryable 
					 select new NotifyTargetDto
					{
						Notify_Id=_notifyTarget.Notify_Id,
						To_Id=_notifyTarget.To_Id,
						HaveRead=_notifyTarget.HaveRead,
						Id=_notifyTarget.Id,
					};
			return query;
		}
		
	}
}
