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
	///邮件 服务类 
	/// </summary>
	public partial class EmailService:BaseService<Email, EmailDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Email> emailRepository;
		#endregion
		#region 构造函数
		public EmailService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<Email> emailRepository,IScopeServiceLoader loader)
			:base(emailRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.emailRepository=emailRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<EmailDto> QueryMain() 
		{
			var emailQueryable=emailRepository.Queryable;
			 var query = from _email in emailQueryable 
					 select new EmailDto
					{
						Title=_email.Title,
						Replay_Email_Id=_email.Replay_Email_Id,
						FromUser_Id=_email.FromUser_Id,
						Id=_email.Id,
					};
			return query;
		}
		#endregion
	}
}
