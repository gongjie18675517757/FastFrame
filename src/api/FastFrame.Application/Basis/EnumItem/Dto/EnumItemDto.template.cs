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
	public partial class EnumItemDto:BaseDto<EnumItem>
	{
		
		
		/// <summary>
		/// 是否系统枚举 
		/// </summary>
		[ReadOnly(ReadOnlyMark.All)]
		public bool IsSystemEnum {get;set;}
		
		/// <summary>
		/// 启用状态 
		/// </summary>
		[EnumItem(EnumName.EnabledMark)]
		public int Enabled {get;set;}
		
		/// <summary>
		/// 字典类别 
		/// </summary>
		[Required()]
		[EnumItem(EnumName.EnumNames)]
		public int? KeyEnum {get;set;}
		
		/// <summary>
		/// 上级 
		/// </summary>
		[RelatedTo<EnumItem>()]
		public string Super_Id {get;set;}
		
		/// <summary>
		/// 上级 
		/// </summary>
		[ValueRelateFor(nameof(Super_Id),typeof(EnumItem))]
		public string Super_Value {get;set;}
		
		/// <summary>
		/// 字典数字值 
		/// </summary>
		[Required()]
		[Unique("KeyEnum")]
		public int? IntKey {get;set;}
		
		/// <summary>
		/// 字典文本值 
		/// </summary>
		[StringLength(150)]
		[Required()]
		[Unique("KeyEnum","Super_Id")]
		public string TextValue {get;set;}
		
		/// <summary>
		/// 排序 
		/// </summary>
		public int SortVal {get;set;}
		
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
