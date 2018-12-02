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
	///邮件正文 服务类 
	/// </summary>
	public partial class EmailContentService:BaseService<EmailContent, EmailContentDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EmailContent> emailContentRepository;
		#endregion
		#region 构造函数
		public EmailContentService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<EmailContent> emailContentRepository,IScopeServiceLoader loader)
			:base(emailContentRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.emailContentRepository=emailContentRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
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
		#endregion
	}
}
