	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 用户 
	/// </summary>
	public partial class UserViewModel:IViewModel
	{
		
		protected UserViewModel()
		{
		}
		
		/// <summary>
		/// 姓名 
		/// </summary>
		public string Name {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
