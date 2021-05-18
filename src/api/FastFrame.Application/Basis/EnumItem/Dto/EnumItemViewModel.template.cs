	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 数字字典 
	/// </summary>
	public partial class EnumItemViewModel:IViewModel
	{
		
		protected EnumItemViewModel()
		{
		}
		
		/// <summary>
		/// 值 
		/// </summary>
		public string Value {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
