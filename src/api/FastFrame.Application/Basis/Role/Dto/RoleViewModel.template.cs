	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 角色 
	/// </summary>
	public partial class RoleViewModel:IViewModel
	{
		
		protected RoleViewModel()
		{
		}
		
		/// <summary>
		/// 名称 
		/// </summary>
		public string Name {get;set;}
		
		/// <summary>
		/// 编码 
		/// </summary>
		public string EnCode {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
