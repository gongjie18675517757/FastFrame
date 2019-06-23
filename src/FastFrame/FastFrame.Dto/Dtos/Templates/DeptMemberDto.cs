namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	/// <summary>
	///部门成员 
	/// </summary>
	public partial class DeptMemberDto:BaseDto<DeptMember>
	{
		
		
		/// <summary>
		///用户 
		/// </summary>
		[Required()]
		public string User_Id {get;set;}
		
		/// <summary>
		///是否管理员 
		/// </summary>
		public bool IsManager {get;set;}
		
		
		
	}
}
