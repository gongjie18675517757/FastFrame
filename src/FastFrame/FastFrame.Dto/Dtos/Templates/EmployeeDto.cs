namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///员工表 
	/// <summary>
	[RelatedField("EnCode","Name")]
	public partial class EmployeeDto:BaseDto<Employee>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///编码 
		/// <summary>
		[StringLength(20)]
		[Required()]
		public string EnCode {get;set;}
		
		/// <summary>
		///名称 
		/// <summary>
		[StringLength(20)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///邮箱 
		/// <summary>
		[StringLength(50)]
		[EmailAddress()]
		public string Email {get;set;}
		
		/// <summary>
		///手机号码 
		/// <summary>
		[StringLength(20)]
		[Phone()]
		public string PhoneNumber {get;set;}
		
		/// <summary>
		///性别 
		/// <summary>
		public GenderMark Gender {get;set;}
		
		/// <summary>
		///用户 
		/// <summary>
		[RelatedTo(typeof(User))]
		public string User_Id {get;set;}
		
		/// <summary>
		///用户帐号 
		/// <summary>
		[RelatedFrom(nameof(User_Id),nameof(User.Account),true)]
		public string User_Account {get;set;}
		
		/// <summary>
		///用户姓名 
		/// <summary>
		[RelatedFrom(nameof(User_Id),nameof(User.Name),false)]
		public string User_Name {get;set;}
		
		/// <summary>
		///部门 
		/// <summary>
		[RelatedTo(typeof(Dept))]
		public string Dept_Id {get;set;}
		
		/// <summary>
		///部门编码 
		/// <summary>
		[RelatedFrom(nameof(Dept_Id),nameof(Dept.EnCode),true)]
		public string Dept_EnCode {get;set;}
		
		/// <summary>
		///部门名称 
		/// <summary>
		[RelatedFrom(nameof(Dept_Id),nameof(Dept.Name),false)]
		public string Dept_Name {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
