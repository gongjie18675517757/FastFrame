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
	public partial class OaLeaveService(IRepository<User> userRepository,IRepository<Dept> deptRepository,IRepository<OaLeave> oaLeaveRepository,IServiceProvider loader):BaseService<OaLeave, OaLeaveDto>(loader,oaLeaveRepository)
	{
		private readonly IRepository<User> userRepository=userRepository;
		private readonly IRepository<Dept> deptRepository=deptRepository;
		private readonly IRepository<OaLeave> oaLeaveRepository=oaLeaveRepository;
		
		
		protected override IQueryable<OaLeaveDto> DefaultQueryable() 
		{
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var deptQueryable = deptRepository.Queryable.Select(Dept.BuildExpression());
			var repository = oaLeaveRepository.Queryable;
			var query = from _oaLeave in repository 
						join _create_User_Id in userQueryable on _oaLeave.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _dept_Id in deptQueryable on _oaLeave.Dept_Id equals _dept_Id.Id into t__dept_Id
						from _dept_Id in t__dept_Id.DefaultIfEmpty()
						join _agent_Id in userQueryable on _oaLeave.Agent_Id equals _agent_Id.Id into t__agent_Id
						from _agent_Id in t__agent_Id.DefaultIfEmpty()
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
							Create_User_Value = _create_User_Id.Value,
							Dept_Value = _dept_Id.Value,
							Agent_Value = _agent_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		
	}
}
