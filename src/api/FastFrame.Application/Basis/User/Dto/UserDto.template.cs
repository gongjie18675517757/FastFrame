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
	/// 用户 
	/// </summary>
	public partial class UserDto:BaseDto<User>
	{
		
		
		/// <summary>
		/// 帐号 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Account {get;set;}
		
		/// <summary>
		/// 密码 
		/// </summary>
		[StringLength(50)]
		[Required()]
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
		[StringLength(200)]
		public string HandIcon_Id {get;set;}
		
		/// <summary>
		/// 头像 
		/// </summary>
		public ResourceViewModel HandIcon {get;set;}
		
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
