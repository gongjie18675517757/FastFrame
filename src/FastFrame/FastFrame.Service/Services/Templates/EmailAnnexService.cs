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
	///邮件附件 服务类 
	/// </summary>
	public partial class EmailAnnexService:BaseService<EmailAnnex, EmailAnnexDto>
	{
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EmailAnnex> emailAnnexRepository;
		
		/*构造函数*/
		public EmailAnnexService(IRepository<User> userRepository,IRepository<EmailAnnex> emailAnnexRepository,IScopeServiceLoader loader)
			:base(emailAnnexRepository,loader)
		{
			this.userRepository=userRepository;
			this.emailAnnexRepository=emailAnnexRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<EmailAnnexDto> QueryMain() 
		{
			var emailAnnexQueryable=emailAnnexRepository.Queryable;
			 var query = from _emailAnnex in emailAnnexQueryable 
					 select new EmailAnnexDto
					{
						Email_Id=_emailAnnex.Email_Id,
						Resource_Id=_emailAnnex.Resource_Id,
						Id=_emailAnnex.Id,
					};
			return query;
		}
		
	}
}
