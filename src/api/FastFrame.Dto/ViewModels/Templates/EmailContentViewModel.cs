namespace FastFrame.Dto.Chat
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///邮件正文 
	/// </summary>
	public partial class EmailContentViewModel:IViewModel
	{
		
		
		/// <summary>
		///邮件ID 
		/// </summary>
		public string Email_Id {get;set;}
		
		/// <summary>
		///内容 
		/// </summary>
		public string Content {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
