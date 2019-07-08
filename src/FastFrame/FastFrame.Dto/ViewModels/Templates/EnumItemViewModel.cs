namespace FastFrame.Dto.Basis
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///数字字典 
	/// </summary>
	public partial class EnumItemViewModel:IViewModel
	{
		
		
		/// <summary>
		///值 
		/// </summary>
		public string Value {get;set;}
		
		/// <summary>
		///键 
		/// </summary>
		public EnumName Key {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
