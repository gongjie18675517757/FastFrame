namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///权限 
	/// </summary>
	[RelatedField("Description","Name","AreaName")]
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
		///名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///区域 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string AreaName {get;set;}
		
		/// <summary>
		///说明 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Description {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
