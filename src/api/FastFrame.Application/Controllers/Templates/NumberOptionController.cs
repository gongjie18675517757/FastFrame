namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///编号设置 
	/// </summary>
	[Permission(nameof(NumberOption),"编号设置")]
	public partial class NumberOptionController:BaseCURDController<NumberOption, NumberOptionDto>
	{
		private readonly NumberOptionService service;
		
		public NumberOptionController(NumberOptionService service)
			:base(service)
		{
			this.service = service;
		}
		
		
		
	}
}
