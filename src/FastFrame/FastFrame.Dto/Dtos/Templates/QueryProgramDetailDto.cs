namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///查询方案明细 
	/// </summary>
	public partial class QueryProgramDetailDto:BaseDto<QueryProgramDetail>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///方案ID 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string SearchProgram_Id {get;set;}
		
		/// <summary>
		///条件名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///比较操作符 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Compare {get;set;}
		
		/// <summary>
		///比较的值 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Value {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
