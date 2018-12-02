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
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<EmailTarget> emailTargetRepository;
		#endregion
		#region 构造函数
		public EmailTargetService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<EmailTarget> emailTargetRepository,IScopeServiceLoader loader)
			:base(emailTargetRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.emailTargetRepository=emailTargetRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<EmailTargetDto> QueryMain() 
		{
			var emailTargetQueryable=emailTargetRepository.Queryable;
			 var query = from emailTarget in emailTargetQueryable 
					 select new EmailTargetDto
					{
						Email_Id=emailTarget.Email_Id,
						Category=emailTarget.Category,
						To_Id=emailTarget.To_Id,
						HaveRead=emailTarget.HaveRead,
						Id=emailTarget.Id,
					};
			return query;
		}
		#endregion
	}
}
