namespace FastFrame.Application.Flow
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Entity.Flow; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
		
	/// <summary>
	/// 工作流 服务实现 
	/// </summary>
	public partial class WorkFlowService:BaseService<WorkFlow, WorkFlowDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<WorkFlow> workFlowRepository;
		
		public WorkFlowService(IRepository<User> userRepository,IRepository<WorkFlow> workFlowRepository)
			 : base(workFlowRepository)
		{
			this.userRepository=userRepository;
			this.workFlowRepository=workFlowRepository;
		}
		
		protected override IQueryable<WorkFlowDto> QueryMain() 
		{
			var userQueryable = userRepository.Queryable.MapTo<User,UserViewModel>();
			var repository = workFlowRepository.Queryable;
			var query = from _workFlow in repository 
						join _create_User_Id in userQueryable on _workFlow.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _workFlow.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new WorkFlowDto
						{
							Name = _workFlow.Name,
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
							Create_User = _create_User_Id,
							Modify_User = _modify_User_Id,
						};
			return query;
		}
		public Task<PageList<WorkFlowViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = workFlowRepository.MapTo<WorkFlow, WorkFlowViewModel>();
			return query.PageListAsync(page);
		}
		
	}
}
