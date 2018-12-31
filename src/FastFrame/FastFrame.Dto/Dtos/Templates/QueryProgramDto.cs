namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using System; 
	/// <summary>
	///查询方案 
	/// </summary>
	public partial class QueryProgramDto:BaseDto<QueryProgram>
	{
		/*字段*/
		
		/*构造函数*/
		
		/*属性*/
		/// <summary>
		///方案名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		///模块名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string ModuleName {get;set;}
		
		/// <summary>
		///是否公共方案 
		/// </summary>
		public bool IsPublic {get;set;}
		
		/// <summary>
		///用户 
		/// </summary>
		public string User_Id {get;set;}
		
		
		/*方法*/
		
	}
}
