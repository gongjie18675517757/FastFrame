	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure; 
	using System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity; 
	using System.Collections.Generic; 
	using System; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 数字字典 
	/// </summary>
	public partial class EnumItemDto:BaseDto<EnumItem>,ITreeModel
	{
		
		
		/// <summary>
		/// 字段类别 
		/// </summary>
		[Required()]
		public EnumName? Key {get;set;}
		
		/// <summary>
		/// 上级值 
		/// </summary>
		[RelatedTo(typeof(EnumItem))]
		public string Super_Id {get;set;}
		
		/// <summary>
		/// 上级值 
		/// </summary>
		public string Super_Value {get;set;}
		
		/// <summary>
		/// 树状码 
		/// </summary>
		[StringLength(20)]
		[IsPrimaryField()]
		public string TreeCode {get;set;}
		
		/// <summary>
		/// 字典值 
		/// </summary>
		[StringLength(150)]
		[Required()]
		public string Value {get;set;}
		
		/// <summary>
		/// 字典键 
		/// </summary>
		public int? IntKey {get;set;}
		
		/// <summary>
		/// 排序 
		/// </summary>
		public int SortVal {get;set;}
		
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
		
		/// <summary>
		/// 下级数量 
		/// </summary>
		public int ChildCount {get;set;}
		
		
	}
}
