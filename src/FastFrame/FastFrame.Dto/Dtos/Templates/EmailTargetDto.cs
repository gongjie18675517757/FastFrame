namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///邮件收件人 
	/// </summary>
	public partial class EmailTargetDto:BaseDto<EmailTarget>
	{
		
		
		/// <summary>
		///邮件ID 
		/// </summary>
		public string Email_Id {get;set;}
		
		/// <summary>
		///类型 
		/// </summary>
		public EmailTargetCategory Category {get;set;}
		
		/// <summary>
		///接收人 
		/// </summary>
		public string To_Id {get;set;}
		
		/// <summary>
		///已读 
		/// </summary>
		public bool HaveRead {get;set;}
		
		
		
	}
}
