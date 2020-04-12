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
	///邮件附件 服务实现 
	/// </summary>
	public partial class EmailAnnexService:BaseService<EmailAnnex, EmailAnnexDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EmailAnnex> emailAnnexRepository;
		
		public EmailAnnexService(IRepository<User> userRepository,IRepository<EmailAnnex> emailAnnexRepository)
			:base(emailAnnexRepository)
		{
			this.userRepository=userRepository;
			this.emailAnnexRepository=emailAnnexRepository;
		}
		
		
		protected override IQueryable<EmailAnnexDto> QueryMain() 
		{
			var query = from _emailAnnex in emailAnnexRepository 
						 select new EmailAnnexDto
						{
							Email_Id=_emailAnnex.Email_Id,
							Resource_Id=_emailAnnex.Resource_Id,
							Id=_emailAnnex.Id,
					};
			return query;
		}
		public  Task<PageList<EmailAnnexViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = from _emailAnnex in emailAnnexRepository 
						select new EmailAnnexViewModel
						{
							Email_Id = _emailAnnex.Email_Id,
							Resource_Id = _emailAnnex.Resource_Id,
							Id = _emailAnnex.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
