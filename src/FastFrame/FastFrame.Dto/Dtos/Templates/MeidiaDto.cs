namespace FastFrame.Dto.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///图片库 
	/// </summary>
	[RelatedField("Name")]
	public partial class MeidiaDto:BaseDto<Meidia>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///链接 
		/// </summary>
		[StringLength(200)]
		public string Href {get;set;}
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(50)]
		public string Name {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
