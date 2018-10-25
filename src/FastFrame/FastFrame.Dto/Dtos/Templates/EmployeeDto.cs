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
		///部门 
		/// <summary>
		[RelatedTo(typeof(Dept))]
		public string Dept_Id {get;set;}
		
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
