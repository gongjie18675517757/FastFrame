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
	/// 图片库 
	/// </summary>
	public partial class MeidiaDto:BaseDto<Meidia>,ITreeModel
	{
		
		
		/// <summary>
		/// 上级 
		/// </summary>
		[RelatedTo(typeof(Meidia))]
		public string Super_Id {get;set;}
		
		/// <summary>
		/// 上级 
		/// </summary>
		public string Super_Value {get;set;}
		
		/// <summary>
		/// 名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[IsPrimaryField()]
		public string Name {get;set;}
		
		/// <summary>
		/// 资源 
		/// </summary>
		public string Resource_Id {get;set;}
		
		/// <summary>
		/// 是否文件夹 
		/// </summary>
		public bool IsFolder {get;set;}
		
		/// <summary>
		/// 树状码 
		/// </summary>
		[StringLength(200)]
		public string TreeCode {get;set;}
		
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
