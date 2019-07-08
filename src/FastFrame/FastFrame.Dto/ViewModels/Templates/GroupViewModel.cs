namespace FastFrame.Dto.Chat
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///群组 
	/// </summary>
	public partial class GroupViewModel:IViewModel
	{
		
		
		/// <summary>
		///群组名称 
		/// </summary>
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
		public string Summary {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
