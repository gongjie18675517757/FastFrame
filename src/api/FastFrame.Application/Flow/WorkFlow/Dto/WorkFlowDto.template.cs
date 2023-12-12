	using FastFrame.Entity.Flow; 
	using FastFrame.Infrastructure; 
	using System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Entity; 
	using System.Collections.Generic; 
	using System; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.Flow
{
		
	/// <summary>
	/// 工作流 
	/// </summary>
	public partial class WorkFlowDto:BaseDto<WorkFlow>
	{
		
		
		/// <summary>
		/// 适用模块 
		/// </summary>
		[StringLength(100)]
		[ReadOnly(ReadOnlyMark.Edit)]
		public string BeModule {get;set;}
		
		/// <summary>
		/// 模块名称 
		/// </summary>
		[StringLength(150)]
		[IsPrimaryField()]
		[ReadOnly(ReadOnlyMark.All)]
		public string BeModuleName {get;set;}
		
		/// <summary>
		/// 版本 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		public int Version {get;set;}
		
		/// <summary>
		/// 状态 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		[EnumItem(EnumName.EnabledMark)]
		public int Enabled {get;set;}
		
		/// <summary>
		/// 备注 
		/// </summary>
		[StringLength(500)]
		public string Remarks {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		[RelatedTo<User>()]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		[ValueRelateFor(nameof(Create_User_Id),typeof(User))]
		public string Create_User_Value {get;set;}
		
		/// <summary>
		/// 创建时间 
		/// </summary>
		[Required()]
		[Hide(HideMark.Form)]
		[ReadOnly(ReadOnlyMark.All)]
		public DateTime CreateTime {get;set;}
		
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
		
		
	}
}
