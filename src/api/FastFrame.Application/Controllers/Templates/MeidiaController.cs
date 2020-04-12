namespace FastFrame.Application.Controllers.Basis
{
	using FastFrame.Dto.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Service.Services.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using FastFrame.Infrastructure.Interface; 
	/// <summary>
	///图片库 
	/// </summary>
	[Permission(nameof(Meidia),"图片库")]
	public partial class MeidiaController:BaseCURDController<Meidia, MeidiaDto>
	{
		private readonly MeidiaService service;
		
		public MeidiaController(MeidiaService service)
			:base(service)
		{
			this.service = service;
		}
		
		
		
	}
}
