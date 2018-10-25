namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///部门 
	/// <summary>
	[RelatedField("EnCode","Name")]
	public partial class DeptDto:BaseDto<Dept>
	{
		/// <summary>
		///上级部门 
		/// <summary>
		[StringLength(50)]
		[RelatedTo(typeof(Dept))]
		public string Parent_Id {get;set;}
		
		/// <summary>
		///编码 
		/// <summary>
		[StringLength(50)]
		[Required()]
		[Unique()]
		public string EnCode {get;set;}
		
		/// <summary>
		///部门名称 
		/// <summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///部门主管 
		/// <summary>
		[StringLength(50)]
		[RelatedTo(typeof(Employee))]
		public string Supervisor_Id {get;set;}
		
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
