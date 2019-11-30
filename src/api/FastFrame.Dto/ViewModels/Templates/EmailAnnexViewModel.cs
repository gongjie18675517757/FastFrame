namespace FastFrame.Dto.Chat
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///邮件附件 
	/// </summary>
	public partial class EmailAnnexViewModel:IViewModel
	{
		
		
		/// <summary>
		///邮件ID 
		/// </summary>
		public string Email_Id {get;set;}
		
		/// <summary>
		///文件ID 
		/// </summary>
		public string Resource_Id {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
