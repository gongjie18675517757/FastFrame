namespace FastFrame.Dto.Chat
{
	using System; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Chat; 
	/// <summary>
	///邮件收件人 
	/// </summary>
	public partial class EmailTargetViewModel:IViewModel
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
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
