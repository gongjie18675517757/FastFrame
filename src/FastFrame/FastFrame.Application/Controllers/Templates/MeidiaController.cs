namespace FastFrame.Application.Controllers.CMS
{
	using FastFrame.Dto.CMS; 
	using FastFrame.Entity.CMS; 
	using FastFrame.Service.Services.CMS; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///图片库 
	/// </summary>
	[Permission(nameof(Meidia),"图片库")]
	public partial class MeidiaController:BaseController<Meidia, MeidiaDto>
	{
		#region 字段
		private readonly MeidiaService service;
		private readonly IScopeServiceLoader serviceLoader;
		#endregion
		#region 构造函数
		public MeidiaController(MeidiaService service,IScopeServiceLoader serviceLoader)
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
