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
	///部门 服务类 
	/// </summary>
	public partial class DeptService:BaseService<Dept, DeptDto>
	{
		private readonly IRepository<Dept> deptRepository;
		private readonly IRepository<User> userRepository;
		
		public DeptService(IRepository<Dept> deptRepository,IRepository<User> userRepository,IScopeServiceLoader loader)
			:base(deptRepository,loader)
		{
			this.deptRepository=deptRepository;
			this.userRepository=userRepository;
		}
		
		
		protected override IQueryable<DeptDto> QueryMain() 
		{
			 var deptQueryable = deptRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _dept in deptQueryable 
						join _parent_Id in deptQueryable.TagWith("_parent_Id") on _dept.Parent_Id equals _parent_Id.Id into t__parent_Id
						from _parent_Id in t__parent_Id.DefaultIfEmpty()
						join _supervisor_Id in userQueryable.TagWith("_supervisor_Id") on _dept.Supervisor_Id equals _supervisor_Id.Id into t__supervisor_Id
						from _supervisor_Id in t__supervisor_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.TagWith("_create_User_Id") on _dept.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.TagWith("_modify_User_Id") on _dept.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						 select new DeptDto
						{
							EnCode=_dept.EnCode,
							Name=_dept.Name,
							Parent_Id=_dept.Parent_Id,
							Supervisor_Id=_dept.Supervisor_Id,
							Id=_dept.Id,
							Create_User_Id=_dept.Create_User_Id,
							CreateTime=_dept.CreateTime,
							Modify_User_Id=_dept.Modify_User_Id,
							ModifyTime=_dept.ModifyTime,
							Parent=_parent_Id==null?null:new DeptDto
							{
								Id = _parent_Id.Id,
								EnCode = _parent_Id.EnCode,
								Name = _parent_Id.Name,
								Parent_Id = _parent_Id.Parent_Id,
								Parent = null,
								Supervisor_Id = _parent_Id.Supervisor_Id,
								Supervisor = null,
								Create_User_Id = _parent_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _parent_Id.CreateTime,
								Modify_User_Id = _parent_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _parent_Id.ModifyTime,
							},
							Supervisor=_supervisor_Id==null?null:new UserDto
							{
								Id = _supervisor_Id.Id,
								Account = _supervisor_Id.Account,
								Password = _supervisor_Id.Password,
								Name = _supervisor_Id.Name,
								Email = _supervisor_Id.Email,
								PhoneNumber = _supervisor_Id.PhoneNumber,
								Dept_Id = _supervisor_Id.Dept_Id,
								Dept = null,
								HandIcon_Id = _supervisor_Id.HandIcon_Id,
								HandIcon = null,
								IsAdmin = _supervisor_Id.IsAdmin,
								IsDisabled = _supervisor_Id.IsDisabled,
								Create_User_Id = _supervisor_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _supervisor_Id.CreateTime,
								Modify_User_Id = _supervisor_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _supervisor_Id.ModifyTime,
							},
							Create_User=_create_User_Id==null?null:new UserDto
							{
								Id = _create_User_Id.Id,
								Account = _create_User_Id.Account,
								Password = _create_User_Id.Password,
								Name = _create_User_Id.Name,
								Email = _create_User_Id.Email,
								PhoneNumber = _create_User_Id.PhoneNumber,
								Dept_Id = _create_User_Id.Dept_Id,
								Dept = null,
								HandIcon_Id = _create_User_Id.HandIcon_Id,
								HandIcon = null,
								IsAdmin = _create_User_Id.IsAdmin,
								IsDisabled = _create_User_Id.IsDisabled,
								Create_User_Id = _create_User_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _create_User_Id.CreateTime,
								Modify_User_Id = _create_User_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _create_User_Id.ModifyTime,
							},
							Modify_User=_modify_User_Id==null?null:new UserDto
							{
								Id = _modify_User_Id.Id,
								Account = _modify_User_Id.Account,
								Password = _modify_User_Id.Password,
								Name = _modify_User_Id.Name,
								Email = _modify_User_Id.Email,
								PhoneNumber = _modify_User_Id.PhoneNumber,
								Dept_Id = _modify_User_Id.Dept_Id,
								Dept = null,
								HandIcon_Id = _modify_User_Id.HandIcon_Id,
								HandIcon = null,
								IsAdmin = _modify_User_Id.IsAdmin,
								IsDisabled = _modify_User_Id.IsDisabled,
								Create_User_Id = _modify_User_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _modify_User_Id.CreateTime,
								Modify_User_Id = _modify_User_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _modify_User_Id.ModifyTime,
							},
					};
			return query;
		}
		
	}
}
