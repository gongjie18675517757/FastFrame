namespace FastFrame.Service.Services.Module
{
	using FastFrame.Entity.Module; 
	using FastFrame.Dto.Module; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using FastFrame.Entity.Basis; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	/// <summary>
	///规则 服务类 
	/// </summary>
	public partial class StrucRuleService:BaseService<StrucRule, StrucRuleDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<StrucRule> strucRuleRepository;
		
		public StrucRuleService(IRepository<User> userRepository,IRepository<StrucRule> strucRuleRepository,IScopeServiceLoader loader)
			:base(strucRuleRepository,loader)
		{
			this.userRepository=userRepository;
			this.strucRuleRepository=strucRuleRepository;
		}
		
		
		protected override IQueryable<StrucRuleDto> QueryMain() 
		{
			var strucRuleQueryable=strucRuleRepository.Queryable;
			 var query = from _strucRule in strucRuleQueryable 
						 select new StrucRuleDto
						{
							RuleName=_strucRule.RuleName,
							Field_Id=_strucRule.Field_Id,
							Id=_strucRule.Id,
					};
			return query;
		}
		
	}
}
