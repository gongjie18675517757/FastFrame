namespace FastFrame.Application.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Application.Basis; 
		
	/// <summary>
	/// 通知 
	/// </summary>
	public partial class NotifyDto:BaseDto<Notify>
	{
		
		
		/// <summary>
		/// 标题 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Title {get;set;}
		
		/// <summary>
		/// 类型 
		/// </summary>
		public string Type_Id {get;set;}
		
		/// <summary>
		/// 发布人 
		/// </summary>
		public string Publush_Id {get;set;}
		
		/// <summary>
		/// 发布人 
		/// </summary>
		public UserViewModel Publush {get;set;}
		
		/// <summary>
		/// 附件 
		/// </summary>
		public string Resource_Id {get;set;}
		
		/// <summary>
		/// 附件 
		/// </summary>
		public ResourceViewModel Resource {get;set;}
		
		/// <summary>
		/// 内容 
		/// </summary>
		[Required()]
		public string Content {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		public UserViewModel Create_User {get;set;}
		
		/// <summary>
		/// 创建时间 
		/// </summary>
		[Required()]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		public UserViewModel Modify_User {get;set;}
		
		/// <summary>
		/// 修改时间 
		/// </summary>
		public DateTime ModifyTime {get;set;}
		
		
	}
}
