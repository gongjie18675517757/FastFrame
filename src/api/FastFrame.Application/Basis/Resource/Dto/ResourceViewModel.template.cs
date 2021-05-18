	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 资源 
	/// </summary>
	public partial class ResourceViewModel:IViewModel
	{
		
		protected ResourceViewModel()
		{
		}
		
		/// <summary>
		/// 资源名称 
		/// </summary>
		public string Name {get;set;}
		
		/// <summary>
		///  
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
