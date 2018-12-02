namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///群组 
	/// </summary>
	public partial class GroupDto:BaseDto<Group>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
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
