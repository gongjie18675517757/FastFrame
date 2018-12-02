namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	/// <summary>
	///邮件正文 
	/// </summary>
	public partial class EmailContentDto:BaseDto<EmailContent>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///邮件ID 
		/// </summary>
		public string Email_Id {get;set;}
		
		/// <summary>
		///内容 
		/// </summary>
		public string Content {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
