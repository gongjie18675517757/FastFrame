namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	/// <summary>
	///部门成员 服务类 
	/// </summary>
	public partial class DeptMemberService:BaseService<DeptMember, DeptMemberDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<DeptMember> deptMemberRepository;
		
		public DeptMemberService(IRepository<User> userRepository,IRepository<DeptMember> deptMemberRepository,IScopeServiceLoader loader)
			:base(deptMemberRepository,loader)
		{
			this.userRepository=userRepository;
			this.deptMemberRepository=deptMemberRepository;
		}
		
		
		protected override IQueryable<DeptMemberDto> QueryMain() 
		{
			var deptMemberQueryable=deptMemberRepository.Queryable;
			 var query = from _deptMember in deptMemberQueryable 
						 select new DeptMemberDto
						{
							Id=_deptMember.Id,
							User_Id=_deptMember.User_Id,
							IsManager=_deptMember.IsManager,
					};
			return query;
		}
		
	}
}
