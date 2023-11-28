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
	/// 工作流 服务实现 
	/// </summary>
	public partial class WorkFlowService(IRepository<User> userRepository,IRepository<WorkFlow> workFlowRepository,IServiceProvider loader):BaseService<WorkFlow, WorkFlowDto>(loader,workFlowRepository)
	{
		private readonly IRepository<User> userRepository=userRepository;
		private readonly IRepository<WorkFlow> workFlowRepository=workFlowRepository;
		
		
		protected override IQueryable<WorkFlowDto> DefaultQueryable() 
		{
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var repository = workFlowRepository.Queryable;
			var query = from _workFlow in repository 
						join _create_User_Id in userQueryable on _workFlow.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _workFlow.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new WorkFlowDto
						{
							BeModule = _workFlow.BeModule,
							BeModuleName = _workFlow.BeModuleName,
							Version = _workFlow.Version,
							Enabled = _workFlow.Enabled,
							Remarks = _workFlow.Remarks,
							Id = _workFlow.Id,
							Create_User_Id = _workFlow.Create_User_Id,
							CreateTime = _workFlow.CreateTime,
							Modify_User_Id = _workFlow.Modify_User_Id,
							ModifyTime = _workFlow.ModifyTime,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		
	}
}
