namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	/// <summary>
	///通知 
	/// </summary>
	public partial class NotifyDto:BaseDto<Notify>
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
		///内容 
		/// </summary>
		[StringLength(500)]
		[Required()]
		public string Content {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
