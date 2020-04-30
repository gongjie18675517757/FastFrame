namespace FastFrame.Application.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
		
	/// <summary>
	/// 组织信息 
	/// </summary>
	public partial class TenantDto:BaseDto<Tenant>
	{
		
		
		/// <summary>
		/// 全称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string FullName {get;set;}
		
		/// <summary>
		/// 简称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string ShortName {get;set;}
		
		/// <summary>
		/// URL标识 
		/// </summary>
		[StringLength(50)]
		public string UrlMark {get;set;}
		
		/// <summary>
		/// 上级 
		/// </summary>
		public string Super_Id {get;set;}
		
		/// <summary>
		/// 租户标记 
		/// </summary>
		public string Tenant_Id {get;set;}
		
		/// <summary>
		/// Logo头像 
		/// </summary>
		public string HandIcon_Id {get;set;}
		
		
	}
}
