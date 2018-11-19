namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///角色权限 
	/// </summary>
	public partial class RolePermissionDto:BaseDto<RolePermission>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///角色ID 
		/// </summary>
		public string Role_Id {get;set;}
		
		/// <summary>
		///父级 
		/// </summary>
		public string Parent_Id {get;set;}
		
		/// <summary>
		///权限ID 
		/// </summary>
		public string Permission_Id {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
