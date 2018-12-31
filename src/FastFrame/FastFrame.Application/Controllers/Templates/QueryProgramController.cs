namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///查询方案 
	/// </summary>
	[Permission(nameof(QueryProgram),"查询方案")]
	public partial class QueryProgramController:BaseController<QueryProgram, QueryProgramDto>
	{
		/*字段*/
		private readonly QueryProgramService service;
		private readonly IScopeServiceLoader serviceLoader;
		
		/*构造函数*/
		public QueryProgramController(QueryProgramService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		
		/*属性*/
		
		/*方法*/
		
	}
}
