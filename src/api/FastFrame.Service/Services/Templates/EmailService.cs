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
	///邮件 服务实现 
	/// </summary>
	public partial class EmailService:BaseService<Email, EmailDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Email> emailRepository;
		
		public EmailService(IRepository<User> userRepository,IRepository<Email> emailRepository,IScopeServiceLoader loader)
			:base(emailRepository,loader)
		{
			this.userRepository=userRepository;
			this.emailRepository=emailRepository;
		}
		
		
		protected override IQueryable<EmailDto> QueryMain() 
		{
			var query = from _email in emailRepository 
						 select new EmailDto
						{
							Title=_email.Title,
							Replay_Email_Id=_email.Replay_Email_Id,
							FromUser_Id=_email.FromUser_Id,
							Id=_email.Id,
					};
			return query;
		}
		public  Task<PageList<EmailViewModel>> ViewModelListAsync(PagePara page) 
		{
			var query = from _email in emailRepository 
						select new EmailViewModel
						{
							Title = _email.Title,
							Replay_Email_Id = _email.Replay_Email_Id,
							FromUser_Id = _email.FromUser_Id,
							Id = _email.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
