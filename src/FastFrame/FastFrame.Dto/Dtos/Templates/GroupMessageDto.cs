namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	/// <summary>
	///群组消息 
	/// </summary>
	public partial class GroupMessageDto:BaseDto<GroupMessage>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///群组ID 
		/// </summary>
		public string Group_Id {get;set;}
		
		/// <summary>
		///内容 
		/// </summary>
		[StringLength(500)]
		[Required()]
		public string Content {get;set;}
		
		/// <summary>
		///消息类型 
		/// </summary>
		public MsgType Category {get;set;}
		
		/// <summary>
		///图片?附件ID 
		/// </summary>
		public string Resource_Id {get;set;}
		
		/// <summary>
		///发送人 
		/// </summary>
		public string From_Id {get;set;}
		
		/// <summary>
		///消息时间 
		/// </summary>
		public DateTime MessageTime {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
