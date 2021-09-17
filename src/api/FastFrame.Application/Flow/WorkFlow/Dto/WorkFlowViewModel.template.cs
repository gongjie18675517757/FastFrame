	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Flow
{
		
	/// <summary>
	/// 工作流 
	/// </summary>
	public partial class WorkFlowViewModel:IViewModel
	{
		
		protected WorkFlowViewModel()
		{
		}
		
		/// <summary>
		/// 适用模块 
		/// </summary>
		public string BeModule {get;set;}
		
		/// <summary>
		/// 版本 
		/// </summary>
		public int Version {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
