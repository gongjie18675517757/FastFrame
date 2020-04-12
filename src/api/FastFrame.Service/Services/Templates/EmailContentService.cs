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
	///邮件正文 服务实现 
	/// </summary>
	public partial class EmailContentService:BaseService<EmailContent, EmailContentDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EmailContent> emailContentRepository;
		
		public EmailContentService(IRepository<User> userRepository,IRepository<EmailContent> emailContentRepository)
			:base(emailContentRepository)
		{
			this.userRepository=userRepository;
			this.emailContentRepository=emailContentRepository;
		}
		
		
		protected override IQueryable<EmailContentDto> QueryMain() 
		{
			var query = from _emailContent in emailContentRepository 
						 select new EmailContentDto
						{
							Email_Id=_emailContent.Email_Id,
							Content=_emailContent.Content,
							Id=_emailContent.Id,
					};
			return query;
		}
		public  Task<PageList<EmailContentViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = from _emailContent in emailContentRepository 
						select new EmailContentViewModel
						{
							Email_Id = _emailContent.Email_Id,
							Content = _emailContent.Content,
							Id = _emailContent.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
