namespace FastFrame.Dto.Module
{
	using FastFrame.Entity.Module; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///键值对 
	/// </summary>
	public partial class EntityKeyValueDto:BaseDto<EntityKeyValue>
	{
		
		
		/// <summary>
		///来源ID 
		/// </summary>
		public string Source_Id {get;set;}
		
		/// <summary>
		///键 
		/// </summary>
		[StringLength(150)]
		[Required()]
		public string Key {get;set;}
		
		/// <summary>
		///值 
		/// </summary>
		[StringLength(150)]
		public string Value {get;set;}
		
		
		
	}
}
