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
	///邮件正文 服务类 
	/// </summary>
	public partial class EmailContentService:BaseService<EmailContent, EmailContentDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EmailContent> emailContentRepository;
		
		public EmailContentService(IRepository<User> userRepository,IRepository<EmailContent> emailContentRepository,IScopeServiceLoader loader)
			:base(emailContentRepository,loader)
		{
			this.userRepository=userRepository;
			this.emailContentRepository=emailContentRepository;
		}
		
		
		protected override IQueryable<EmailContentDto> QueryMain() 
		{
			var emailContentQueryable=emailContentRepository.Queryable;
			 var query = from _emailContent in emailContentQueryable 
						 select new EmailContentDto
						{
							Email_Id=_emailContent.Email_Id,
							Content=_emailContent.Content,
							Id=_emailContent.Id,
					};
			return query;
		}
		
	}
}
