namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///群组 
	/// </summary>
	public partial class GroupDto:BaseDto<Group>
	{
		/*字段*/
		
		/*构造函数*/
		
		/*属性*/
		/// <summary>
		///群组名称 
		/// </summary>
		[StringLength(50)]
		public string Name {get;set;}
		
		/// <summary>
		///群主 
		/// </summary>
		public string LordUser_Id {get;set;}
		
		/// <summary>
		///图标 
		/// </summary>
		public string HandIcon_Id {get;set;}
		
		/// <summary>
		///简介 
		/// </summary>
		[StringLength(200)]
		public string Summary {get;set;}
		
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
