namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///角色 
	/// </summary>
	[RelatedField("Name","EnCode")]
	public partial class RoleDto:BaseDto<Role>
	{
		/*字段*/
		
		/*构造函数*/
		
		/*属性*/
		/// <summary>
		///编码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Unique()]
		public string EnCode {get;set;}
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
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
