namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///邮件正文 
	/// </summary>
	public partial class EmailContentDto:BaseDto<EmailContent>
	{
		
		
		/// <summary>
		///邮件ID 
		/// </summary>
		public string Email_Id {get;set;}
		
		/// <summary>
		///内容 
		/// </summary>
		public string Content {get;set;}
		
		
		
	}
}
