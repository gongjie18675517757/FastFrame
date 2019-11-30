namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	/// <summary>
	///通知目标 
	/// </summary>
	public partial class NotifyTargetDto:BaseDto<NotifyTarget>
	{
		
		
		/// <summary>
		///通知ID 
		/// </summary>
		[Required()]
		public string Notify_Id {get;set;}
		
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
