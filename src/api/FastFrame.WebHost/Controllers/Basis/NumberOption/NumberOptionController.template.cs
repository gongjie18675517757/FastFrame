	using FastFrame.Application.Basis; 
namespace FastFrame.WebHost.Controllers.Basis
{
		
	/// <summary>
	/// 编号设置 
	/// </summary>
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
