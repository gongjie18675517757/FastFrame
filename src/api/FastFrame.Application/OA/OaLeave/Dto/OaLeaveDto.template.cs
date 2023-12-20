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
		[ReadOnly(ReadOnlyMark.All)]
		public string Number {get;set;}
		
		/// <summary>
		/// 申请时间 
		/// </summary>
		[Required()]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		/// 申请人 
		/// </summary>
		[Required()]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo<User>()]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 申请人 
		/// </summary>
		[ValueRelateFor(nameof(Create_User_Id),typeof(User))]
		public string Create_User_Value {get;set;}
		
		/// <summary>
		/// 岗位 
		/// </summary>
		[Required()]
		[EnumItem(EnumName.Job)]
		public int? Job_Id {get;set;}
		
		/// <summary>
		/// 部门 
		/// </summary>
		[Required()]
		[RelatedTo<Dept>()]
		public string Dept_Id {get;set;}
		
		/// <summary>
		/// 部门 
		/// </summary>
		[ValueRelateFor(nameof(Dept_Id),typeof(Dept))]
		public string Dept_Value {get;set;}
		
		/// <summary>
		/// 请假类型 
		/// </summary>
		[Required()]
		[EnumItem(EnumName.LeaveCategoryEnum)]
		public int? LeaveCategory {get;set;}
		
		/// <summary>
		/// 工作代理人 
		/// </summary>
		[Required()]
		[RelatedTo<User>()]
		public string Agent_Id {get;set;}
		
		/// <summary>
		/// 工作代理人 
		/// </summary>
		[ValueRelateFor(nameof(Agent_Id),typeof(User))]
		public string Agent_Value {get;set;}
		
		/// <summary>
		/// 请假日期起 
		/// </summary>
		[Hide(HideMark.All)]
		public DateTime? BeginDateTime {get;set;}
		
		/// <summary>
		/// 请假日期止 
		/// </summary>
		[Hide(HideMark.All)]
		public DateTime? EndDateTime {get;set;}
		
		/// <summary>
		/// 请假日期 
		/// </summary>
		[Required()]
		public ValueRange<DateTime> DateTime
		{
			get
			{
				return new ValueRange<DateTime>(BeginDateTime,EndDateTime);
			}
			set
			{
				BeginDateTime=value.BeginValue;
				EndDateTime=value.EndValue;
			}
		}
		
		/// <summary>
		/// 请假日期起 
		/// </summary>
		[Hide(HideMark.All)]
		public TimeOnly? BeginTimeOnly {get;set;}
		
		/// <summary>
		/// 请假日期止 
		/// </summary>
		[Hide(HideMark.All)]
		public TimeOnly? EndTimeOnly {get;set;}
		
		/// <summary>
		/// 请假日期 
		/// </summary>
		[Required()]
		public ValueRange<TimeOnly> TimeOnly
		{
			get
			{
				return new ValueRange<TimeOnly>(BeginTimeOnly,EndTimeOnly);
			}
			set
			{
				BeginTimeOnly=value.BeginValue;
				EndTimeOnly=value.EndValue;
			}
		}
		
		/// <summary>
		/// 请假天数起 
		/// </summary>
		[Hide(HideMark.All)]
		public int? BeginDays {get;set;}
		
		/// <summary>
		/// 请假天数止 
		/// </summary>
		[Hide(HideMark.All)]
		public int? EndDays {get;set;}
		
		/// <summary>
		/// 请假天数 
		/// </summary>
		[Required()]
		public ValueRange<int> Days
		{
			get
			{
				return new ValueRange<int>(BeginDays,EndDays);
			}
			set
			{
				BeginDays=value.BeginValue;
				EndDays=value.EndValue;
			}
		}
		
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
		[ReadOnly(ReadOnlyMark.All)]
		[EnumItem(EnumName.FlowStatusEnum)]
		public int FlowStatus {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo<User>()]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		[ValueRelateFor(nameof(Modify_User_Id),typeof(User))]
		public string Modify_User_Value {get;set;}
		
		/// <summary>
		/// 修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
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
