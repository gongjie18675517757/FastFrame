namespace FastFrame.Dto.Flow
{
	using FastFrame.Entity.Flow; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///工作流 
	/// </summary>
	[RelatedField("Name")]
	public partial class WorkFlowDto:BaseDto<WorkFlow>
	{
		
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(100)]
		[Required()]
		[Unique()]
		public string Name {get;set;}
		
		/// <summary>
		///适用模块 
		/// </summary>
		[StringLength(100)]
		[Unique()]
		[ReadOnly(ReadOnlyMark.Edit)]
		public string BeModule {get;set;}
		
		/// <summary>
		///备注 
		/// </summary>
		[StringLength(500)]
		public string Remarks {get;set;}
		
		/// <summary>
		///创建人 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		///创建人 
		/// </summary>
		public UserViewModel Create_User {get;set;}
		
		/// <summary>
		///创建时间 
		/// </summary>
		[Required()]
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo(typeof(User))]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		///修改人 
		/// </summary>
		public UserViewModel Modify_User {get;set;}
		
		/// <summary>
		///修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime ModifyTime {get;set;}
		
		
		
	}
}
