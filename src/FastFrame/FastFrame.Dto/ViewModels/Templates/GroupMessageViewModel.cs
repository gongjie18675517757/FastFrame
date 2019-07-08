namespace FastFrame.Dto.Chat
{
	using System; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Chat; 
	/// <summary>
	///群组消息 
	/// </summary>
	public partial class GroupMessageViewModel:IViewModel
	{
		
		
		/// <summary>
		///群组ID 
		/// </summary>
		public string Group_Id {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
