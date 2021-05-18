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
	/// 编号设置 
	/// </summary>
	public partial class NumberOptionDto:BaseDto<NumberOption>
	{
		
		
		/// <summary>
		/// 模块名称 
		/// </summary>
		[StringLength(100)]
		[Required()]
		public string BeModule {get;set;}
		
		/// <summary>
		/// 前缀 
		/// </summary>
		[StringLength(10)]
		public string Prefix {get;set;}
		
		/// <summary>
		/// 是否取日期 
		/// </summary>
		public bool TaskDate {get;set;}
		
		/// <summary>
		/// 流水号长度 
		/// </summary>
		public int SerialLength {get;set;}
		
		/// <summary>
		/// 后缀 
		/// </summary>
		[StringLength(10)]
		public string Suffix {get;set;}
		
		/// <summary>
		/// 取日期字段 
		/// </summary>
		[StringLength(50)]
		public string DateField {get;set;}
		
		/// <summary>
		/// 日期字段名称 
		/// </summary>
		[StringLength(50)]
		public string DateFieldText {get;set;}
		
		/// <summary>
		/// 日期格式方法 
		/// </summary>
		public FmtDateEnum FmtDate {get;set;}
		
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
