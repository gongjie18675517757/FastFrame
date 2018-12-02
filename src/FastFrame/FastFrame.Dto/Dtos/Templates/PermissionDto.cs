namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///权限 
	/// </summary>
	[RelatedField("Name","EnCode","AreaName")]
	public partial class PermissionDto:BaseDto<Permission>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///父级 
		/// </summary>
		[RelatedTo(typeof(Permission))]
		public string Parent_Id {get;set;}
		
		/// <summary>
		///父级 
		/// </summary>
		public PermissionDto Parent {get;set;}
		
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
		///名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
