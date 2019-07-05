namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	/// <summary>
	/// 
	/// </summary>
	public partial class TenantHostDto:BaseDto<TenantHost>
	{
		
		
		/// <summary>
		///域名 
		/// </summary>
		[StringLength(200)]
		[Required()]
		public string Host {get;set;}
		
		
		
	}
}
