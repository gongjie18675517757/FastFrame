namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///通知 
	/// </summary>
	public partial class NotifyDto:BaseDto<Notify>
	{
		
		
		/// <summary>
		///标题 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Title {get;set;}
		
		/// <summary>
		///发布人 
		/// </summary>
		[RelatedTo(typeof(User))]
		public string Publush_Id {get;set;}
		
		/// <summary>
		///发布人 
		/// </summary>
		public UserDto Publush {get;set;}
		
		/// <summary>
		///附件 
		/// </summary>
		[RelatedTo(typeof(Resource))]
		public string Resource_Id {get;set;}
		
		/// <summary>
		///附件 
		/// </summary>
		public ResourceDto Resource {get;set;}
		
		/// <summary>
		///内容 
		/// </summary>
		[StringLength(8000)]
		[Required()]
		public string Content {get;set;}
		
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
		public UserDto Create_User {get;set;}
		
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
		public UserDto Modify_User {get;set;}
		
		/// <summary>
		///修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime ModifyTime {get;set;}
	}
}
