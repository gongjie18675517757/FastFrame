namespace FastFrame.Dto.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	using FastFrame.Dto.CMS; 
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
		
		/// <summary>
		///管理属性 
		/// </summary>
		public Foreign Foreign {get;set;}
		
		/// <summary>
		///创建人 
		/// </summary>
		public User Create_User {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		public User Modify_User {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
