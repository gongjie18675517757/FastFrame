namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///查询方案 
	/// <summary>
	public partial class QueryProgramController:BaseController<QueryProgram, QueryProgramDto>
	{
		#region 字段
		private readonly QueryProgramService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public QueryProgramController(QueryProgramService service,IScopeServiceLoader serviceLoader)
			:base(service,serviceLoader)
		{
			this.service = service;
			this.serviceLoader = serviceLoader;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		#endregion
	}
}
