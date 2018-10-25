namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///查询方案 
	/// <summary>
	public partial class QueryProgramDto:BaseDto<QueryProgram>
	{
		/// <summary>
		///模块名称 
		/// <summary>
		[StringLength(50)]
		[Required()]
		public string ModuleName {get;set;}
		
		/// <summary>
		///方案名称 
		/// <summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///是否公共方案 
		/// <summary>
		public bool IsPublic {get;set;}
		
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
