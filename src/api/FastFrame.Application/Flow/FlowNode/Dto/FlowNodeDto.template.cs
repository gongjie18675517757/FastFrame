namespace FastFrame.Application.Flow
{
	using FastFrame.Entity.Flow; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System.Collections.Generic; 
	using System; 
		
	/// <summary>
	/// 流程节点 
	/// </summary>
	public partial class FlowNodeDto:BaseDto<FlowNode>
	{
		
		
		/// <summary>
		/// 关联:WorkFlow 
		/// </summary>
		public string WorkFlow_Id {get;set;}
		
		/// <summary>
		/// 名称 
		/// </summary>
		[StringLength(100)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		/// 节点键 
		/// </summary>
		public int Key {get;set;}
		
		/// <summary>
		/// 主管审核 
		/// </summary>
		public bool DeptCheck {get;set;}
		
		/// <summary>
		/// 触发应用通知 
		/// </summary>
		public bool TriggerAppNotify {get;set;}
		
		/// <summary>
		/// 触发微信通知 
		/// </summary>
		public bool TriggerWxNotify {get;set;}
		
		/// <summary>
		/// 触发短信通知 
		/// </summary>
		public bool TriggerSmsNotify {get;set;}
		
		
	}
}
