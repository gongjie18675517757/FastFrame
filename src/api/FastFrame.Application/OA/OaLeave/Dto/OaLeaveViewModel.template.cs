	using System; 
	using FastFrame.Entity.Enums; 
namespace FastFrame.Application.OA
{
		
	/// <summary>
	/// 请假单 
	/// </summary>
	public partial class OaLeaveViewModel:IViewModel
	{
		
		protected OaLeaveViewModel()
		{
		}
		
		/// <summary>
		/// 请假单号 
		/// </summary>
		public string Number {get;set;}
		
		/// <summary>
		/// 岗位 
		/// </summary>
		public string Job_Id {get;set;}
		
		/// <summary>
		/// 部门 
		/// </summary>
		public string Dept_Id {get;set;}
		
		/// <summary>
		/// 请假类型 
		/// </summary>
		public LeaveCategoryEnum? LeaveCategory {get;set;}
		
		/// <summary>
		/// 工作代理人 
		/// </summary>
		public string Agent_Id {get;set;}
		
		/// <summary>
		/// 开始时间 
		/// </summary>
		public DateTime? StartTime {get;set;}
		
		/// <summary>
		/// 结束时间 
		/// </summary>
		public DateTime? EndTime {get;set;}
		
		/// <summary>
		/// 请假天数 
		/// </summary>
		public Decimal? Days {get;set;}
		
		/// <summary>
		/// 申请事由 
		/// </summary>
		public string Reasons {get;set;}
		
		/// <summary>
		/// 审批状态 
		/// </summary>
		public FlowStatusEnum FlowStatus {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
