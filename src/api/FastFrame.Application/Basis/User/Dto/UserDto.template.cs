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
	/// 用户 
	/// </summary>
	public partial class UserDto:BaseDto<User>
	{
		
		
		/// <summary>
		/// 帐号 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[IsPrimaryField()]
		public string Account {get;set;}
		
		/// <summary>
		/// 密码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Hide(HideMark.List)]
		public string Password {get;set;}
		
		/// <summary>
		/// 姓名 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		/// 邮箱 
		/// </summary>
		[StringLength(50)]
		public string Email {get;set;}
		
		/// <summary>
		/// 手机号码 
		/// </summary>
		[StringLength(20)]
		public string PhoneNumber {get;set;}
		
		/// <summary>
		/// 头像 
		/// </summary>
		[Hide(HideMark.Form)]
		[RelatedTo(typeof(Resource))]
		public string HandIcon_Id {get;set;}
		
		/// <summary>
		/// 头像 
		/// </summary>
		public string HandIcon_Value {get;set;}
		
		/// <summary>
		/// 是否管理员 
		/// </summary>
		public bool IsAdmin {get;set;}
		
		/// <summary>
		/// 启用状态 
		/// </summary>
		public EnabledMark Enable {get;set;}
		
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
