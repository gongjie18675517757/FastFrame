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
	///邮件收件人 服务类 
	/// </summary>
	public partial class EmailTargetService:BaseService<EmailTarget, EmailTargetDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EmailTarget> emailTargetRepository;
		
		public EmailTargetService(IRepository<User> userRepository,IRepository<EmailTarget> emailTargetRepository,IScopeServiceLoader loader)
			:base(emailTargetRepository,loader)
		{
			this.userRepository=userRepository;
			this.emailTargetRepository=emailTargetRepository;
		}
		
		
		protected override IQueryable<EmailTargetDto> QueryMain() 
		{
			var emailTargetQueryable=emailTargetRepository.Queryable;
			 var query = from _emailTarget in emailTargetQueryable 
					 select new EmailTargetDto
					{
						Email_Id=_emailTarget.Email_Id,
						Category=_emailTarget.Category,
						To_Id=_emailTarget.To_Id,
						HaveRead=_emailTarget.HaveRead,
						Id=_emailTarget.Id,
					};
			return query;
		}
		
	}
}
