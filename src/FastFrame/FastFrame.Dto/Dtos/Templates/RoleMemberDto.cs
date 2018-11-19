namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///角色成员 
	/// </summary>
	public partial class RoleMemberDto:BaseDto<RoleMember>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///角色 
		/// </summary>
		public string Role_Id {get;set;}
		
		/// <summary>
		///用户 
		/// </summary>
		public string User_Id {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
