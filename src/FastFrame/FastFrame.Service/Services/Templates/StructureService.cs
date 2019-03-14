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
	///结构 服务类 
	/// </summary>
	public partial class StructureService:BaseService<Structure, StructureDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Structure> structureRepository;
		
		public StructureService(IRepository<User> userRepository,IRepository<Structure> structureRepository,IScopeServiceLoader loader)
			:base(structureRepository,loader)
		{
			this.userRepository=userRepository;
			this.structureRepository=structureRepository;
		}
		
		
		protected override IQueryable<StructureDto> QueryMain() 
		{
			var structureQueryable=structureRepository.Queryable;
			 var query = from _structure in structureQueryable 
						 select new StructureDto
						{
							Name=_structure.Name,
							Description=_structure.Description,
							TreeKey_Id=_structure.TreeKey_Id,
							HasManage=_structure.HasManage,
							Id=_structure.Id,
					};
			return query;
		}
		
	}
}
