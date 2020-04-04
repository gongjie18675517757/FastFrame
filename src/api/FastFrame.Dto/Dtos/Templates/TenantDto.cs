namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	/// <summary>
	///组织信息 
	/// </summary>
	public partial class TenantDto:BaseDto<Tenant>
	{
		
		
		/// <summary>
		///全称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string FullName {get;set;}
		
		/// <summary>
		///简称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string ShortName {get;set;}
		
		/// <summary>
		///URL标识 
		/// </summary>
		[StringLength(50)]
		[Unique()]
		public string UrlMark {get;set;}
		
		/// <summary>
		///上级 
		/// </summary>
		public string Super_Id {get;set;}
		
		/// <summary>
		///是否可发展下级 
		/// </summary>
		public bool CanHaveChildren {get;set;}
		
		/// <summary>
		///头像 
		/// </summary>
		public string HandIcon_Id {get;set;}
		
		
		
	}
}
