	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure; 
	using System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity; 
	using System.Collections.Generic; 
	using System; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 多租户信息 
	/// </summary>
	public partial class TenantDto:BaseDto<Tenant>,ITreeModel
	{
		
		
		/// <summary>
		/// 全称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[IsPrimaryField()]
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
		
		/// <summary>
		/// 树状码 
		/// </summary>
		[StringLength(200)]
		public string TreeCode {get;set;}
		
		/// <summary>
		/// 下级数量 
		/// </summary>
		public int ChildCount {get;set;}
		
		
	}
}
