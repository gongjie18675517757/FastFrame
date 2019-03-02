namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	/// <summary>
	///资源 
	/// </summary>
	public partial class ResourceDto:BaseDto<Resource>
	{
		
		
		/// <summary>
		///资源名称 
		/// </summary>
		[StringLength(150)]
		public string Name {get;set;}
		
		/// <summary>
		///资源大小 
		/// </summary>
		public long Size {get;set;}
		
		/// <summary>
		///相对路径 
		/// </summary>
		[StringLength(150)]
		public string Path {get;set;}
		
		/// <summary>
		///资源标识 
		/// </summary>
		[StringLength(50)]
		public string ContentType {get;set;}
		
		/// <summary>
		///MD5摘要 
		/// </summary>
		public string MD5 {get;set;}
		
		
		
	}
}
