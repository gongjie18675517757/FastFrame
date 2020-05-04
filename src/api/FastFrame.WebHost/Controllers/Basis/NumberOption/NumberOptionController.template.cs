namespace FastFrame.WebHost.Controllers.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Application.Basis; 
	using FastFrame.Infrastructure.Permission; 
	using FastFrame.Infrastructure.Interface; 
		
	/// <summary>
	/// 编号设置 
	/// </summary>
	[Permission(nameof(NumberOption),"编号设置")]
	public partial class NumberOptionController:BaseCURDController<NumberOptionDto>
	{
		private readonly NumberOptionService service;
		
		public NumberOptionController(NumberOptionService service)
			 : base(service)
		{
			this.service = service;
		}
		
		
	}
}
