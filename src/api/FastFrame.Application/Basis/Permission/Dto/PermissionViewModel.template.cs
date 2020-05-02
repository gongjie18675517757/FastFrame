namespace FastFrame.Application.Basis
{
	using System; 
	using FastFrame.Entity.Enums; 
		
	/// <summary>
	/// 权限 
	/// </summary>
	public partial class PermissionViewModel:IViewModel
	{
		
		protected PermissionViewModel()
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
