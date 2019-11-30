namespace FastFrame.Dto.Basis
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	/// 
	/// </summary>
	public partial class TenantHostViewModel:IViewModel
	{
		
		
		/// <summary>
		///域名 
		/// </summary>
		public string Host {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		/// <summary>
		///租户 
		/// </summary>
		public string Tenant_Id {get;set;}
		
		
		
	}
}
