namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	/// <summary>
	///通知目标 服务实现 
	/// </summary>
	public partial class NotifyTargetService:BaseService<NotifyTarget, NotifyTargetDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<NotifyTarget> notifyTargetRepository;
		
		public NotifyTargetService(IRepository<User> userRepository,IRepository<NotifyTarget> notifyTargetRepository,IScopeServiceLoader loader)
			:base(notifyTargetRepository,loader)
		{
			this.userRepository=userRepository;
			this.notifyTargetRepository=notifyTargetRepository;
		}
		
		
		protected override IQueryable<NotifyTargetDto> QueryMain() 
		{
			 var query = from _notifyTarget in notifyTargetRepository 
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
