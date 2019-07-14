namespace FastFrame.Dto.Chat
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///邮件 
	/// </summary>
	public partial class EmailViewModel:IViewModel
	{
		
		
		/// <summary>
		///标题 
		/// </summary>
		public string Title {get;set;}
		
		/// <summary>
		///回复自 
		/// </summary>
		public string Replay_Email_Id {get;set;}
		
		/// <summary>
		///发件人 
		/// </summary>
		public string FromUser_Id {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
