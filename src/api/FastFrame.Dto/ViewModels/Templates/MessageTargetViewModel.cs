namespace FastFrame.Dto.Chat
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///消息接收人 
	/// </summary>
	public partial class MessageTargetViewModel:IViewModel
	{
		
		
		/// <summary>
		///消息ID 
		/// </summary>
		public string Message_Id {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
