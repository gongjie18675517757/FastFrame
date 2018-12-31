namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///资源 
	/// </summary>
	public partial class ResourceDto:BaseDto<Resource>
	{
		/*字段*/
		
		/*构造函数*/
		
		/*属性*/
		/// <summary>
		///资源名称 
		/// </summary>
		[StringLength(150)]
		public string Name {get;set;}
		
		/// <summary>
		///资源大小 
		/// </summary>
		public long Size {get;set;}
		
		/// <summary>
		///相对路径 
		/// </summary>
		[StringLength(150)]
		public string Path {get;set;}
		
		/// <summary>
		///资源标识 
		/// </summary>
		public string ContentType {get;set;}
		
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
