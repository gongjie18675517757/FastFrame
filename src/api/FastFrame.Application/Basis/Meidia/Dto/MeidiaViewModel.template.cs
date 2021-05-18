	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 图片库 
	/// </summary>
	public partial class MeidiaViewModel:IViewModel
	{
		
		protected MeidiaViewModel()
		{
		}
		
		/// <summary>
		/// 名称 
		/// </summary>
		public string Name {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
