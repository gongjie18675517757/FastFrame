	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 组织信息 
	/// </summary>
	public partial class TenantViewModel:IViewModel
	{
		
		protected TenantViewModel()
		{
		}
		
		/// <summary>
		/// 全称 
		/// </summary>
		public string FullName {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
