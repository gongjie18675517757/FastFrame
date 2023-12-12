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
		[Unique()]
		[ReadOnly(ReadOnlyMark.Edit)]
		public string Account {get;set;}
		
		/// <summary>
		/// 密码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Hide(HideMark.List)]
		[ReadOnly(ReadOnlyMark.Edit)]
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
		[Unique()]
		public string Email {get;set;}
		
		/// <summary>
		/// 手机号码 
		/// </summary>
		[StringLength(20)]
		[Unique()]
		public string PhoneNumber {get;set;}
		
		/// <summary>
		/// 头像 
		/// </summary>
		[RelatedTo<Resource>()]
		public string HandIcon_Id {get;set;}
		
		/// <summary>
		/// 头像 
		/// </summary>
		[ValueRelateFor(nameof(HandIcon_Id),typeof(Resource))]
		public string HandIcon_Value {get;set;}
		
		/// <summary>
		/// 是否管理员 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		public bool IsAdmin {get;set;}
		
		/// <summary>
		/// 启用状态 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		[EnumItem(EnumName.EnabledMark)]
		public int Enable {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo<User>()]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		[ValueRelateFor(nameof(Create_User_Id),typeof(User))]
		public string Create_User_Value {get;set;}
		
		/// <summary>
		/// 创建时间 
		/// </summary>
		[Required()]
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo<User>()]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		[ValueRelateFor(nameof(Modify_User_Id),typeof(User))]
		public string Modify_User_Value {get;set;}
		
		/// <summary>
		/// 修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime ModifyTime {get;set;}
		
		
	}
}
