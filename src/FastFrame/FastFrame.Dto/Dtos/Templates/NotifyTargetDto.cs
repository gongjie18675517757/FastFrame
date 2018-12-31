namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///通知目标 
	/// </summary>
	public partial class NotifyTargetDto:BaseDto<NotifyTarget>
	{
		/*字段*/
		
		/*构造函数*/
		
		/*属性*/
		/// <summary>
		///通知ID 
		/// </summary>
		public string Notify_Id {get;set;}
		
		/// <summary>
		///接收人 
		/// </summary>
		public string To_Id {get;set;}
		
		/// <summary>
		///已读 
		/// </summary>
		public bool HaveRead {get;set;}
		
		
		/*方法*/
		
	}
}
