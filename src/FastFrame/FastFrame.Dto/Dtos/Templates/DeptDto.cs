namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///部门 
	/// </summary>
	[RelatedField("Name","EnCode")]
	public partial class DeptDto:BaseDto<Dept>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
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
		///上级 
		/// </summary>
		[StringLength(50)]
		[RelatedTo(typeof(Dept))]
		public string Parent_Id {get;set;}
		
		/// <summary>
		///上级 
		/// </summary>
		public DeptDto Parent {get;set;}
		
		/// <summary>
		///主管 
		/// </summary>
		[StringLength(50)]
		[RelatedTo(typeof(Employee))]
		public string Supervisor_Id {get;set;}
		
		/// <summary>
		///主管 
		/// </summary>
		public EmployeeDto Supervisor {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
