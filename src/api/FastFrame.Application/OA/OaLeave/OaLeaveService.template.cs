	using FastFrame.Entity.Basis; 
	using FastFrame.Entity.OA; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.OA
{
		
	/// <summary>
	/// 请假单 服务实现 
	/// </summary>
	public partial class OaLeaveService:BaseService<OaLeave, OaLeaveDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<OaLeave> oaLeaveRepository;
		
		public OaLeaveService(IRepository<User> userRepository,IRepository<Dept> deptRepository,IRepository<OaLeave> oaLeaveRepository)
			 : base(oaLeaveRepository)
		{
			this.userRepository=userRepository;
			this.deptRepository=deptRepository;
			this.oaLeaveRepository=oaLeaveRepository;
		}
		
		protected override IQueryable<OaLeaveDto> QueryMain() 
		{
			var userQueryable = userRepository.Queryable.MapTo<User,UserViewModel>();
			var deptQueryable = deptRepository.Queryable.MapTo<Dept,DeptViewModel>();
			var repository = oaLeaveRepository.Queryable;
			var query = from _oaLeave in repository 
						join _create_User_Id in userQueryable on _oaLeave.Create_User_Id equals _create_User_Id.Id 
						join _dept_Id in deptQueryable on _oaLeave.Dept_Id equals _dept_Id.Id 
						join _agent_Id in userQueryable on _oaLeave.Agent_Id equals _agent_Id.Id 
						join _modify_User_Id in userQueryable on _oaLeave.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new OaLeaveDto
						{
							Number = _oaLeave.Number,
							CreateTime = _oaLeave.CreateTime,
							Create_User_Id = _oaLeave.Create_User_Id,
							Job_Id = _oaLeave.Job_Id,
							Dept_Id = _oaLeave.Dept_Id,
							LeaveCategory = _oaLeave.LeaveCategory,
							Agent_Id = _oaLeave.Agent_Id,
							StartTime = _oaLeave.StartTime,
							EndTime = _oaLeave.EndTime,
							Days = _oaLeave.Days,
							Reasons = _oaLeave.Reasons,
							FlowStatus = _oaLeave.FlowStatus,
							Id = _oaLeave.Id,
							Modify_User_Id = _oaLeave.Modify_User_Id,
							ModifyTime = _oaLeave.ModifyTime,
							Create_User = _create_User_Id,
							Dept = _dept_Id,
							Agent = _agent_Id,
							Modify_User = _modify_User_Id,
						};
			return query;
		}
		public Task<PageList<OaLeaveViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = oaLeaveRepository.MapTo<OaLeave, OaLeaveViewModel>();
			return query.PageListAsync(page);
		}
		
	}
}
