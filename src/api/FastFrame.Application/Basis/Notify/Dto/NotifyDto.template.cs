	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure; 
	using System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity; 
	using System.Collections.Generic; 
	using System; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.Basis
{
		
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
		[EnumItem("NotifyType")]
		public string Type_Id {get;set;}
		
		/// <summary>
		/// 发布人 
		/// </summary>
		[RelatedTo(typeof(User))]
		public string Publush_Id {get;set;}
		
		/// <summary>
		/// 发布人 
		/// </summary>
		public string Publush_Value {get;set;}
		
		/// <summary>
		/// 附件 
		/// </summary>
		[RelatedTo(typeof(Resource))]
		public string Resource_Id {get;set;}
		
		/// <summary>
		/// 附件 
		/// </summary>
		public string Resource_Value {get;set;}
		
		/// <summary>
		/// 内容 
		/// </summary>
		[Required()]
		public string Content {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		[Hide(HideMark.Form)]
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		public string Create_User_Value {get;set;}
		
		/// <summary>
		/// 创建时间 
		/// </summary>
		[Required()]
		[Hide(HideMark.Form)]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		[Hide(HideMark.Form)]
		[RelatedTo(typeof(User))]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		public string Modify_User_Value {get;set;}
		
		/// <summary>
		/// 修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		public DateTime ModifyTime {get;set;}
		
		
	}
}
