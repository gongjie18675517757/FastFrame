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
	///图片库 
	/// </summary>
	[Unique("Super_Id","Name")]
	[RelatedField("Name")]
	public partial class MeidiaDto:BaseDto<Meidia>
	{
		
		
		/// <summary>
		///上级 
		/// </summary>
		[RelatedTo(typeof(Meidia))]
		public string Super_Id {get;set;}
		
		/// <summary>
		///上级 
		/// </summary>
		public MeidiaViewModel Super {get;set;}
		
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
		public ResourceViewModel Resource {get;set;}
		
		/// <summary>
		///资源标识 
		/// </summary>
		[StringLength(50)]
		public string ContentType {get;set;}
		
		/// <summary>
		///是否文件夹 
		/// </summary>
		public bool IsFolder {get;set;}
		
		/// <summary>
		///创建人 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		///创建人 
		/// </summary>
		public UserViewModel Create_User {get;set;}
		
		/// <summary>
		///创建时间 
		/// </summary>
		[Required()]
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo(typeof(User))]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		public UserViewModel Modify_User {get;set;}
		
		/// <summary>
		///修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime ModifyTime {get;set;}
		
		
		
	}
}
