	using FastFrame.Entity.OA; 
	using FastFrame.Infrastructure; 
	using System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Entity; 
	using System.Collections.Generic; 
	using System; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.OA
{
		
	/// <summary>
	/// 请假单 
	/// </summary>
	public partial class OaLeaveDto:BaseDto<OaLeave>,IHaveMultiFileDto,IHaveCheckModel
	{
		
		
		/// <summary>
		/// 请假单号 
		/// </summary>
		[StringLength(20)]
		public string Number {get;set;}
		
		/// <summary>
		/// 申请时间 
		/// </summary>
		[Required()]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		/// 申请人 
		/// </summary>
		[Required()]
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 申请人 
		/// </summary>
		public UserViewModel Create_User {get;set;}
		
		/// <summary>
		/// 岗位 
		/// </summary>
		[Required()]
		[EnumItem("Job")]
		public string Job_Id {get;set;}
		
		/// <summary>
		/// 部门 
		/// </summary>
		[Required()]
		[RelatedTo(typeof(Dept))]
		public string Dept_Id {get;set;}
		
		/// <summary>
		/// 部门 
		/// </summary>
		public DeptViewModel Dept {get;set;}
		
		/// <summary>
		/// 请假类型 
		/// </summary>
		[Required()]
		public LeaveCategoryEnum? LeaveCategory {get;set;}
		
		/// <summary>
		/// 工作代理人 
		/// </summary>
		[Required()]
		[RelatedTo(typeof(User))]
		public string Agent_Id {get;set;}
		
		/// <summary>
		/// 工作代理人 
		/// </summary>
		public UserViewModel Agent {get;set;}
		
		/// <summary>
		/// 开始时间 
		/// </summary>
		[Required()]
		public DateTime? StartTime {get;set;}
		
		/// <summary>
		/// 结束时间 
		/// </summary>
		[Required()]
		public DateTime? EndTime {get;set;}
		
		/// <summary>
		/// 请假天数 
		/// </summary>
		public Decimal? Days {get;set;}
		
		/// <summary>
		/// 申请事由 
		/// </summary>
		[StringLength(500)]
		public string Reasons {get;set;}
		
		/// <summary>
		/// 审批状态 
		/// </summary>
		public FlowStatusEnum FlowStatus {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		[Hide(HideMark.Form)]
		[RelatedTo(typeof(User))]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		public UserViewModel Modify_User {get;set;}
		
		/// <summary>
		/// 修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		public DateTime ModifyTime {get;set;}
		
		/// <summary>
		/// 附件 
		/// </summary>
		public IEnumerable<ResourceModel> Files {get;set;}
		
		/// <summary>
		/// 可审核人 
		/// </summary>
		public IEnumerable<string> CheckerIds {get;set;}
		
		/// <summary>
		/// 流程步骤 
		/// </summary>
		public IEnumerable<Flow.FlowStepModel> StepList {get;set;}
		
		
	}
}
