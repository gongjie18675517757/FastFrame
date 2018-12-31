namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///用户 
	/// </summary>
	[Unique("Account")]
	[RelatedField("Name","Account")]
	public partial class UserDto:BaseDto<User>
	{
		/*字段*/
		
		/*构造函数*/
		
		/*属性*/
		/// <summary>
		///帐号 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Unique()]
		[ReadOnly(ReadOnlyMark.Edit)]
		public string Account {get;set;}
		
		/// <summary>
		///密码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Hide(HideMark.List)]
		[ReadOnly(ReadOnlyMark.Edit)]
		public string Password {get;set;}
		
		/// <summary>
		///姓名 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///部门 
		/// </summary>
		[RelatedTo(typeof(Dept))]
		public string Dept_Id {get;set;}
		
		/// <summary>
		///部门 
		/// </summary>
		public DeptDto Dept {get;set;}
		
		/// <summary>
		///邮箱 
		/// </summary>
		[StringLength(50)]
		[Unique()]
		public string Email {get;set;}
		
		/// <summary>
		///手机号码 
		/// </summary>
		[StringLength(20)]
		[Unique()]
		public string PhoneNumber {get;set;}
		
		/// <summary>
		///头像 
		/// </summary>
		[StringLength(200)]
		[Hide(HideMark.All)]
		public string HandIconId {get;set;}
		
		/// <summary>
		///是否管理员 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		public bool IsAdmin {get;set;}
		
		/// <summary>
		///是否禁用 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		public bool IsDisabled {get;set;}
		
		/// <summary>
		///创建人 
		/// </summary>
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		///创建人 
		/// </summary>
		public UserDto Create_User {get;set;}
		
		/// <summary>
		///创建时间 
		/// </summary>
		[Required()]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		[RelatedTo(typeof(User))]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		public UserDto Modify_User {get;set;}
		
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime ModifyTime {get;set;}
		
		
		/*方法*/
		
	}
}
