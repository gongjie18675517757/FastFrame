namespace FastFrame.Dto.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums;
    using FastFrame.Entity.Basis;
    using FastFrame.Dto.Basis;

    /// <summary>
    ///图片库 
    /// </summary>
    [Unique("Parent_Id","Name")]
	[RelatedField("Name")]
	public partial class MeidiaDto:BaseDto<Meidia>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///上级 
		/// </summary>
		[RelatedTo(typeof(Meidia))]
		public string Parent_Id {get;set;}
		
		/// <summary>
		///上级 
		/// </summary>
		public MeidiaDto Parent {get;set;}
		
		/// <summary>
		///链接 
		/// </summary>
		[StringLength(200)]
		public string Href {get;set;}
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///资源 
		/// </summary>
		[RelatedTo(typeof(Resource))]
		public string Resource_Id {get;set;}
		
		/// <summary>
		///资源 
		/// </summary>
		public ResourceDto Resource {get;set;}
		
		/// <summary>
		///是否文件夹 
		/// </summary>
		public bool IsFolder {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
