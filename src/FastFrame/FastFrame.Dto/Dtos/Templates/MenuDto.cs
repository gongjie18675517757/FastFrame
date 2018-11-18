namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///菜单 
	/// </summary>
	[RelatedField("Name","EnCode")]
	public partial class MenuDto:BaseDto<Menu>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///编码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string EnCode {get;set;}
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///上级菜单 
		/// </summary>
		[StringLength(50)]
		[RelatedTo(typeof(Menu))]
		public string Parent_Id {get;set;}
		
		/// <summary>
		///上级菜单 
		/// </summary>
		public MenuDto Parent {get;set;}
		
		/// <summary>
		///标题 
		/// </summary>
		[StringLength(50)]
		public string Title {get;set;}
		
		/// <summary>
		///图标 
		/// </summary>
		[StringLength(50)]
		public string Icon {get;set;}
		
		/// <summary>
		///路径 
		/// </summary>
		[StringLength(50)]
		public string Path {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
