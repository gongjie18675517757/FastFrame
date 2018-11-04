namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///组织信息 
	/// <summary>
	public partial class OrganizeController:BaseController<Organize, OrganizeDto>
	{
		#region 字段
		private readonly OrganizeService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public OrganizeController(OrganizeService service,IScopeServiceLoader serviceLoader)
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
