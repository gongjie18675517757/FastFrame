namespace FastFrame.Dto.System
{
	using FastFrame.Entity.System; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	/// <summary>
	///登陆用户 
	/// <summary>
	[Unique("Account")]
	[RelatedField("Account","Name")]
	public partial class UserDto:BaseDto<User>
	{
		/// <summary>
		///帐号 
		/// <summary>
		[StringLength(50)]
		[Required()]
		[Unique()]
		public string Account {get;set;}
		
		/// <summary>
		///密码 
		/// <summary>
		[StringLength(50)]
		[Required()]
		[Hide(HideMark.List)]
		public string Password {get;set;}
		
		/// <summary>
		///姓名 
		/// <summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///邮箱 
		/// <summary>
		[StringLength(50)]
		[Unique()]
		[EmailAddress()]
		public string Email {get;set;}
		
		/// <summary>
		///手机号码 
		/// <summary>
		[StringLength(20)]
		[Unique()]
		[Phone()]
		public string PhoneNumber {get;set;}
		
		/// <summary>
		///头像 
		/// <summary>
		[StringLength(200)]
		[Hide(HideMark.All)]
		public string HandIconId {get;set;}
		
		/// <summary>
		///是否管理员 
		/// <summary>
		[ReadOnly(ReadOnlyMark.All)]
		public bool IsAdmin {get;set;}
		
		/// <summary>
		///是否禁用 
		/// <summary>
		[ReadOnly(ReadOnlyMark.All)]
		public bool IsDisabled {get;set;}
		
		/// <summary>
		///主键 
		/// <summary>
		public string Id {get;set;}
		
		/// <summary>
		///组织 
		/// <summary>
		public string OrganizeId {get;set;}
		
		/// <summary>
		///删除码 
		/// <summary>
		public bool IsDeleted {get;set;}
		
	}
}
