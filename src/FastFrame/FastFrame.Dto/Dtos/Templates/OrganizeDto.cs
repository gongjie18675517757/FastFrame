namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///组织信息 
	/// <summary>
	public partial class OrganizeDto:BaseDto<Organize>
	{
		/// <summary>
		/// 
		/// <summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
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
