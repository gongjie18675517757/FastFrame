	using FastFrame.Entity.Flow; 
	using FastFrame.Infrastructure; 
	using System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Entity; 
	using System.Collections.Generic; 
	using System; 
namespace FastFrame.Application.Flow
{
		
	/// <summary>
	/// 流程连接线 
	/// </summary>
	public partial class FlowLineDto:BaseDto<FlowLine>
	{
		
		
		/// <summary>
		/// 关联:WorkFlow 
		/// </summary>
		public string WorkFlow_Id {get;set;}
		
		/// <summary>
		/// 名称 
		/// </summary>
		[StringLength(100)]
		public string Text {get;set;}
		
		/// <summary>
		/// 权重 
		/// </summary>
		public Decimal Weights {get;set;}
		
		/// <summary>
		/// 从 
		/// </summary>
		[Required()]
		public int From {get;set;}
		
		/// <summary>
		/// 到 
		/// </summary>
		[Required()]
		public int To {get;set;}
		
		
	}
}
