namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	/// <summary>
	/// 
	/// </summary>
	public partial class EnumItemDto:BaseDto<EnumItem>
	{
		
		
		/// <summary>
		///键 
		/// </summary>
		[StringLength(150)]
		public string EnumName {get;set;}
		
		/// <summary>
		///值 
		/// </summary>
		[StringLength(200)]
		public string EnumValue {get;set;}
		
		/// <summary>
		///上级 
		/// </summary>
		public string Parent_Id {get;set;}
		
		
		
	}
}
