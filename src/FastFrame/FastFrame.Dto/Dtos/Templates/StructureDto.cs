namespace FastFrame.Dto.Module
{
	using FastFrame.Entity.Module; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///结构 
	/// </summary>
	public partial class StructureDto:BaseDto<Structure>
	{
		
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(150)]
		[Required()]
		[ReadOnly(ReadOnlyMark.All)]
		public string Name {get;set;}
		
		/// <summary>
		///说明 
		/// </summary>
		[StringLength(150)]
		[Required()]
		public string Description {get;set;}
		
		/// <summary>
		///树状键 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		public string TreeKey_Id {get;set;}
		
		/// <summary>
		///拥有管理属性 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		public bool HasManage {get;set;}
		
		
		
	}
}
