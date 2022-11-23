	using FastFrame.Entity.Basis; 
	using FastFrame.Entity.Flow; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.Flow
{
		
	/// <summary>
	/// 动态表单模块 服务实现 
	/// </summary>
	public partial class DFModuleService:BaseService<DFModule, DFModuleDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<DFModule> dFModuleRepository;
		
		public DFModuleService(IRepository<User> userRepository,IRepository<DFModule> dFModuleRepository,IServiceProvider loader)
			 : base(loader,dFModuleRepository)
		{
			this.userRepository=userRepository;
			this.dFModuleRepository=dFModuleRepository;
		}
		
		protected override IQueryable<DFModuleDto> DefaultQueryable() 
		{
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var repository = dFModuleRepository.Queryable;
			var query = from _dFModule in repository 
						join _create_User_Id in userQueryable on _dFModule.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _dFModule.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new DFModuleDto
						{
							Name = _dFModule.Name,
							Description = _dFModule.Description,
							Version = _dFModule.Version,
							IsEnabled = _dFModule.IsEnabled,
							HaveCheck = _dFModule.HaveCheck,
							HaveNumber = _dFModule.HaveNumber,
							Id = _dFModule.Id,
							Create_User_Id = _dFModule.Create_User_Id,
							CreateTime = _dFModule.CreateTime,
							Modify_User_Id = _dFModule.Modify_User_Id,
							ModifyTime = _dFModule.ModifyTime,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		
	}
}
