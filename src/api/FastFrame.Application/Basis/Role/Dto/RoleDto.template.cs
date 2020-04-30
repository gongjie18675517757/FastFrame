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
	/// 角色 
	/// </summary>
	public partial class RoleDto:BaseDto<Role>
	{
		
		
		/// <summary>
		/// 编码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string EnCode {get;set;}
		
		/// <summary>
		/// 名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		/// 缺省角色 
		/// </summary>
		public bool IsDefault {get;set;}
		
		/// <summary>
		/// 管理员角色 
		/// </summary>
		public bool IsAdmin {get;set;}
		
		/// <summary>
		/// 备注 
		/// </summary>
		[StringLength(500)]
		public string Remarks {get;set;}
		
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
