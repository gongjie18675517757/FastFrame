namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///邮件附件 
	/// </summary>
	public partial class EmailAnnexDto:BaseDto<EmailAnnex>
	{
		
		
		/// <summary>
		///邮件ID 
		/// </summary>
		public string Email_Id {get;set;}
		
		/// <summary>
		///文件ID 
		/// </summary>
		public string Resource_Id {get;set;}
		
		
		
	}
}
