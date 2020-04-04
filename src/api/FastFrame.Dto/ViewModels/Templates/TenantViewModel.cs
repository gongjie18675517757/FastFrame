namespace FastFrame.Dto.Basis
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///组织信息 
	/// </summary>
	public partial class TenantViewModel:IViewModel
	{
		
		
		/// <summary>
		///全称 
		/// </summary>
		public string FullName {get;set;}
		
		/// <summary>
		///简称 
		/// </summary>
		public string ShortName {get;set;}
		
		/// <summary>
		///URL标识 
		/// </summary>
		public string UrlMark {get;set;}
		
		/// <summary>
		///上级 
		/// </summary>
		public string Super_Id {get;set;}
		
		/// <summary>
		///是否可发展下级 
		/// </summary>
		public bool CanHaveChildren {get;set;}
		
		/// <summary>
		///头像 
		/// </summary>
		public string HandIcon_Id {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
