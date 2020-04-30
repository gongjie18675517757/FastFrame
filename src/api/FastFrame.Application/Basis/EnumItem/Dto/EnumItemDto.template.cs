namespace FastFrame.Application.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Application.Basis; 
		
	/// <summary>
	/// 数字字典 
	/// </summary>
	public partial class EnumItemDto:BaseDto<EnumItem>
	{
		
		
		/// <summary>
		/// 键 
		/// </summary>
		[Required()]
		public EnumName Key {get;set;}
		
		/// <summary>
		/// 值 
		/// </summary>
		[StringLength(150)]
		[Required()]
		public string Value {get;set;}
		
		/// <summary>
		/// 上级 
		/// </summary>
		public string Super_Id {get;set;}
		
		/// <summary>
		/// 上级 
		/// </summary>
		public EnumItemViewModel Super {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		public UserViewModel Create_User {get;set;}
		
		/// <summary>
		/// 创建时间 
		/// </summary>
		[Required()]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		public UserViewModel Modify_User {get;set;}
		
		/// <summary>
		/// 修改时间 
		/// </summary>
		public DateTime ModifyTime {get;set;}
		
		
	}
}
