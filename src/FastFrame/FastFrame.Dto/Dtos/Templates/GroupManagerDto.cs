namespace FastFrame.Dto.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///群组管理员 
	/// </summary>
	public partial class GroupManagerDto:BaseDto<GroupManager>
	{
		/*字段*/
		
		/*构造函数*/
		
		/*属性*/
		/// <summary>
		///群组 
		/// </summary>
		public string Group_Id {get;set;}
		
		/// <summary>
		///管理员 
		/// </summary>
		public string User_Id {get;set;}
		
		
		/*方法*/
		
	}
}
