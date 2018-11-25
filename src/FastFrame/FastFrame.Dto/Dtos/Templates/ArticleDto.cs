namespace FastFrame.Dto.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///文章 
	/// </summary>
	public partial class ArticleDto:BaseDto<Article>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///标题 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Title {get;set;}
		
		/// <summary>
		///文章类别 
		/// </summary>
		[RelatedTo(typeof(ArticleCategory))]
		public string ArticleCategory_Id {get;set;}
		
		/// <summary>
		///文章类别 
		/// </summary>
		public ArticleCategoryDto ArticleCategory {get;set;}
		
		/// <summary>
		///URL标识 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Unique()]
		public string Url {get;set;}
		
		/// <summary>
		///概述 
		/// </summary>
		[StringLength(50)]
		public string Summarize {get;set;}
		
		/// <summary>
		///缩略图 
		/// </summary>
		[Hide(HideMark.List)]
		[RelatedTo(typeof(Meidia))]
		public string Thumbnail_Id {get;set;}
		
		/// <summary>
		///缩略图 
		/// </summary>
		public MeidiaDto Thumbnail {get;set;}
		
		/// <summary>
		///文章内容 
		/// </summary>
		[Required()]
		[Hide(HideMark.List)]
		public string Content {get;set;}
		
		/// <summary>
		///发布? 
		/// </summary>
		public bool IsRelease {get;set;}
		
		/// <summary>
		///阅读次数 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public int LookCount {get;set;}
		
		/// <summary>
		///说明 
		/// </summary>
		[StringLength(500)]
		public string Description {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
