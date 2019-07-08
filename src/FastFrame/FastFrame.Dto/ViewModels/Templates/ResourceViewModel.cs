namespace FastFrame.Dto.Basis
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///资源 
	/// </summary>
	public partial class ResourceViewModel:IViewModel
	{
		
		
		/// <summary>
		///资源名称 
		/// </summary>
		public string Name {get;set;}
		
		/// <summary>
		///资源大小 
		/// </summary>
		public long Size {get;set;}
		
		/// <summary>
		///相对路径 
		/// </summary>
		public string Path {get;set;}
		
		/// <summary>
		///资源标识 
		/// </summary>
		public string ContentType {get;set;}
		
		/// <summary>
		///MD5摘要 
		/// </summary>
		public string MD5 {get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
