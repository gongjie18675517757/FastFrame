namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	/// <summary>
	///邮件附件 
	/// </summary>
	public partial class EmailAnnexDto:BaseDto<EmailAnnex>
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
		///文件ID 
		/// </summary>
		public string Resource_Id {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
