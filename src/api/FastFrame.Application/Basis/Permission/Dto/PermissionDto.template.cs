namespace FastFrame.Application.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Application.Basis; 
		
	/// <summary>
	/// 权限 
	/// </summary>
	public partial class PermissionDto:BaseDto<Permission>
	{
		
		
		/// <summary>
		/// 名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		/// 编码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string EnCode {get;set;}
		
		/// <summary>
		/// 区域 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string AreaName {get;set;}
		
		/// <summary>
		/// 父级 
		/// </summary>
		public string Super_Id {get;set;}
		
		/// <summary>
		/// 父级 
		/// </summary>
		public PermissionViewModel Super {get;set;}
		
		/// <summary>
		/// 是否有下级 
		/// </summary>
		public bool HasTreeChildren {get;set;}
		
		
	}
}
