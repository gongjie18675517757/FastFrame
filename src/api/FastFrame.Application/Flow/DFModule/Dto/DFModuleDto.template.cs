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
	/// 动态表单模块 
	/// </summary>
	public partial class DFModuleDto:BaseDto<DFModule>
	{
		
		
		/// <summary>
		/// 编码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		/// 名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Description {get;set;}
		
		/// <summary>
		/// 版本 
		/// </summary>
		public int Version {get;set;}
		
		/// <summary>
		/// 是否启用 
		/// </summary>
		public bool IsEnabled {get;set;}
		
		/// <summary>
		/// 是否需要审核 
		/// </summary>
		public bool HaveCheck {get;set;}
		
		/// <summary>
		/// 是否需要编号 
		/// </summary>
		public bool HaveNumber {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		[Hide(HideMark.Form)]
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		public string Create_User_Value {get;set;}
		
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
		public string Modify_User_Value {get;set;}
		
		/// <summary>
		/// 修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		public DateTime ModifyTime {get;set;}
		
		
	}
}
