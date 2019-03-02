namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///消息接收人 
	/// </summary>
	public partial class MessageTargetDto:BaseDto<MessageTarget>
	{
		
		
		/// <summary>
		///消息ID 
		/// </summary>
		public string Message_Id {get;set;}
		
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
