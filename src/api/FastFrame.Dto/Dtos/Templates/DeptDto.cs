namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///部门 
	/// </summary>
	[RelatedField("Name","EnCode")]
	public partial class DeptDto:BaseDto<Dept>
	{
		
		
		/// <summary>
		///编码 
		/// </summary>
		[StringLength(50)]
		[Required()]
		[Unique()]
		public string EnCode {get;set;}
		
		/// <summary>
		///名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///主管 
		/// </summary>
		[StringLength(50)]
		[RelatedTo(typeof(User))]
		public string Supervisor_Id {get;set;}
		
		/// <summary>
		///主管 
		/// </summary>
		public UserViewModel Supervisor {get;set;}
		
		/// <summary>
		///上级 
		/// </summary>
		[StringLength(50)]
		[RelatedTo(typeof(Dept))]
		public string Super_Id {get;set;}
		
		/// <summary>
		///上级 
		/// </summary>
		public DeptViewModel Super {get;set;}
		
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
