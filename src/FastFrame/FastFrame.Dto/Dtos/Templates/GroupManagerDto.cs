namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	/// <summary>
	///群组管理员 
	/// </summary>
	public partial class GroupManagerDto:BaseDto<GroupManager>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///群组 
		/// </summary>
		public string Group_Id {get;set;}
		
		/// <summary>
		///管理员 
		/// </summary>
		public string User_Id {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
