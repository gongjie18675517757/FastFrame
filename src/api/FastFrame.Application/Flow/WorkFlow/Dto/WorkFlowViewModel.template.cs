namespace FastFrame.Application.Flow
{
	using System; 
	using FastFrame.Entity.Enums; 
		
	/// <summary>
	/// 工作流 
	/// </summary>
	public partial class WorkFlowViewModel:IViewModel
	{
		
		protected WorkFlowViewModel()
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