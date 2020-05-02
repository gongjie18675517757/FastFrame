namespace FastFrame.Application.Basis
{
	using System; 
	using FastFrame.Entity.Enums; 
		
	/// <summary>
	/// 部门 
	/// </summary>
	public partial class DeptViewModel:IViewModel
	{
		
		protected DeptViewModel()
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
