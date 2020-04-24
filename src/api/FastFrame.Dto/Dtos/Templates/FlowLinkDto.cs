namespace FastFrame.Dto.Flow
{
	using FastFrame.Entity.Flow; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///流程线 
	/// </summary>
	public partial class FlowLinkDto:BaseDto<FlowLink>
	{
		
		
		/// <summary>
		///关联:WorkFlow 
		/// </summary>
		public string WorkFlow_Id {get;set;}
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(100)]
		public string Text {get;set;}
		
		/// <summary>
		///从 
		/// </summary>
		public int From {get;set;}
		
		/// <summary>
		///到 
		/// </summary>
		public int To {get;set;}
		
		
		
	}
}
