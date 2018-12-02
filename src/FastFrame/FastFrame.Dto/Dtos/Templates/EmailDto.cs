namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	/// <summary>
	///邮件 
	/// </summary>
	public partial class EmailDto:BaseDto<Email>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///标题 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Title {get;set;}
		
		/// <summary>
		///回复自 
		/// </summary>
		public string Replay_Email_Id {get;set;}
		
		/// <summary>
		///发件人 
		/// </summary>
		public string FromUser_Id {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
