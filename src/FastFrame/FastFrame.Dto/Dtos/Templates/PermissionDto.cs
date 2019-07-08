namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///权限 
	/// </summary>
	[RelatedField("Name","EnCode")]
	public partial class PermissionDto:BaseDto<Permission>
	{
		
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///编码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string EnCode {get;set;}
		
		/// <summary>
		///区域 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string AreaName {get;set;}
		
		/// <summary>
		///父级 
		/// </summary>
		[RelatedTo(typeof(Permission))]
		public string Super_Id {get;set;}
		
		/// <summary>
		///父级 
		/// </summary>
		public PermissionViewModel Super {get;set;}
		
		
		
	}
}
