namespace FastFrame.Dto.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///文章类别 
	/// </summary>
	[RelatedField("Name")]
	public partial class ArticleCategoryDto:BaseDto<ArticleCategory>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///上级标题 
		/// </summary>
		[RelatedTo(typeof(ArticleCategory))]
		public string Parent_Id {get;set;}
		
		/// <summary>
		///上级标题 
		/// </summary>
		public ArticleCategoryDto Parent {get;set;}
		
		/// <summary>
		///标题 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Unique()]
		public string Name {get;set;}
		
		/// <summary>
		///Url标识 
		/// </summary>
		[StringLength(50)]
		[Unique()]
		public string Url {get;set;}
		
		/// <summary>
		///描述 
		/// </summary>
		[StringLength(500)]
		public string Description {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
