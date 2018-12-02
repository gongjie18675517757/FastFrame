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
	///通知 服务类 
	/// </summary>
	public partial class NotifyService:BaseService<Notify, NotifyDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Notify> notifyRepository;
		#endregion
		#region 构造函数
		public NotifyService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<Notify> notifyRepository,IScopeServiceLoader loader)
			:base(notifyRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.notifyRepository=notifyRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<NotifyDto> QueryMain() 
		{
			var notifyQueryable=notifyRepository.Queryable;
			 var query = from _notify in notifyQueryable 
					 select new NotifyDto
					{
						Title=_notify.Title,
						Content=_notify.Content,
						Id=_notify.Id,
					};
			return query;
		}
		#endregion
	}
}
