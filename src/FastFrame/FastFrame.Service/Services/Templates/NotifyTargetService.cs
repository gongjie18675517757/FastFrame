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
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<NotifyTarget> notifyTargetRepository;
		#endregion
		#region 构造函数
		public NotifyTargetService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<NotifyTarget> notifyTargetRepository,IScopeServiceLoader loader)
			:base(notifyTargetRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.notifyTargetRepository=notifyTargetRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<NotifyTargetDto> QueryMain() 
		{
			var notifyTargetQueryable=notifyTargetRepository.Queryable;
			 var query = from notifyTarget in notifyTargetQueryable 
					 select new NotifyTargetDto
					{
						Notify_Id=notifyTarget.Notify_Id,
						To_Id=notifyTarget.To_Id,
						HaveRead=notifyTarget.HaveRead,
						Id=notifyTarget.Id,
					};
			return query;
		}
		#endregion
	}
}
