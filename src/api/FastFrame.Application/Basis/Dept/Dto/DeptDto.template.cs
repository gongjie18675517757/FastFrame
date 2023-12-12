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
	/// 部门 
	/// </summary>
	public partial class DeptDto:BaseDto<Dept>
	{ 
		/// <summary>
		/// 上级 
		/// </summary>
		[RelatedTo<Dept>()]
		public string Super_Id {get;set;}
		
		/// <summary>
		/// 上级 
		/// </summary>
		[ValueRelateFor(nameof(Super_Id),typeof(Dept))]
		public string Super_Value {get;set;}
		
		/// <summary>
		/// 部门代码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Unique()]
		public string EnCode {get;set;}
		
		/// <summary>
		/// 部门名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[IsPrimaryField()]
		[Unique()]
		public string Name {get;set;}
		
		/// <summary>
		/// 备注 
		/// </summary>
		[StringLength(200)]
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
