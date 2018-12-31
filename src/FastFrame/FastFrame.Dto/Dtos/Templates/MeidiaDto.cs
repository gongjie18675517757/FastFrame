namespace FastFrame.Dto.CMS
{
	using FastFrame.Entity.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	using FastFrame.Dto.CMS; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///图片库 
	/// </summary>
	[Unique("Parent_Id","Name")]
	[RelatedField("Name")]
	public partial class MeidiaDto:BaseDto<Meidia>
	{
		/*字段*/
		
		/*构造函数*/
		
		/*属性*/
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
		
		/// <summary>
		///创建人 
		/// </summary>
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		///创建人 
		/// </summary>
		public UserDto Create_User {get;set;}
		
		/// <summary>
		///创建时间 
		/// </summary>
		[Required()]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		[RelatedTo(typeof(User))]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		public UserDto Modify_User {get;set;}
		
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime ModifyTime {get;set;}
		
		
		/*方法*/
		
	}
}
