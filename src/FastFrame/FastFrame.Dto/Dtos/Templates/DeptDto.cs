namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///部门 
	/// </summary>
	[RelatedField("EnCode","Name")]
	public partial class DeptDto:BaseDto<Dept>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///上级 
		/// </summary>
		[StringLength(50)]
		[RelatedTo(typeof(Dept))]
		public string Parent_Id {get;set;}
		
		/// <summary>
		///上级编码 
		/// </summary>
		[RelatedFrom(nameof(Parent_Id),nameof(Dept.EnCode),true)]
		public string Parent_EnCode {get;set;}
		
		/// <summary>
		///上级名称 
		/// </summary>
		[RelatedFrom(nameof(Parent_Id),nameof(Dept.Name),false)]
		public string Parent_Name {get;set;}
		
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
		[RelatedTo(typeof(Employee))]
		public string Supervisor_Id {get;set;}
		
		/// <summary>
		///主管编码 
		/// </summary>
		[RelatedFrom(nameof(Supervisor_Id),nameof(Employee.EnCode),true)]
		public string Supervisor_EnCode {get;set;}
		
		/// <summary>
		///主管名称 
		/// </summary>
		[RelatedFrom(nameof(Supervisor_Id),nameof(Employee.Name),false)]
		public string Supervisor_Name {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
