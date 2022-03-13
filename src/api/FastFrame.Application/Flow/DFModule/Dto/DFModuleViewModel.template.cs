	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Flow
{
		
	/// <summary>
	/// 动态表单模块 
	/// </summary>
	public partial class DFModuleViewModel:IViewModel
	{
		
		protected DFModuleViewModel()
		{
		}
		
		/// <summary>
		/// 编码 
		/// </summary>
		public string Name {get;set;}
		
		/// <summary>
		/// 名称 
		/// </summary>
		public string Description {get;set;}
		
		/// <summary>
		/// 版本 
		/// </summary>
		public int Version {get;set;}
		
		/// <summary>
		/// 是否启用 
		/// </summary>
		public bool IsEnabled {get;set;}
		
		/// <summary>
		/// 是否需要审核 
		/// </summary>
		public bool HaveCheck {get;set;}
		
		/// <summary>
		/// 是否需要编号 
		/// </summary>
		public bool HaveNumber {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
