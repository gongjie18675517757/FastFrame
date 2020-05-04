namespace FastFrame.Application.Flow
{
	using FastFrame.Entity.Flow; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	using FastFrame.Application.Basis; 
		
	/// <summary>
	/// 工作流 
	/// </summary>
	public partial class WorkFlowDto:BaseDto<WorkFlow>
	{
		
		
		/// <summary>
		/// 名称 
		/// </summary>
		[StringLength(100)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		/// 适用模块 
		/// </summary>
		[StringLength(100)]
		public string BeModule {get;set;}
		
		/// <summary>
		/// 模块名称 
		/// </summary>
		[StringLength(150)]
		public string BeModuleName {get;set;}
		
		/// <summary>
		/// 版本 
		/// </summary>
		public int Version {get;set;}
		
		/// <summary>
		/// 状态 
		/// </summary>
		public EnabledMark Enabled {get;set;}
		
		/// <summary>
		/// 备注 
		/// </summary>
		[StringLength(500)]
		public string Remarks {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		[Hide(HideMark.Form)]
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		public UserViewModel Create_User {get;set;}
		
		/// <summary>
		/// 创建时间 
		/// </summary>
		[Required()]
		[Hide(HideMark.Form)]
		public DateTime CreateTime {get;set;}
		
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
		
		
	}
}
