namespace FastFrame.Dto.Module
{
	using FastFrame.Entity.Module; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	using FastFrame.Entity.Basis; 
	using System; 
	/// <summary>
	///规则 
	/// </summary>
	public partial class StrucRuleDto:BaseDto<StrucRule>
	{
		
		
		/// <summary>
		///规则名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string RuleName {get;set;}
		
		/// <summary>
		/// 
		/// </summary>
		public string Field_Id {get;set;}
		
		
		
	}
}
