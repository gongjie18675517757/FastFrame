namespace FastFrame.Service.Services.Chat
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Entity.Chat; 
	using FastFrame.Dto.Chat; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///通知 服务类 
	/// </summary>
	public partial class NotifyService:BaseService<Notify, NotifyDto>
	{
		private readonly IRepository<Employee> employeeRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Notify> notifyRepository;
		
		public NotifyService(IRepository<Employee> employeeRepository,IRepository<User> userRepository,IRepository<Notify> notifyRepository,IScopeServiceLoader loader)
			:base(notifyRepository,loader)
		{
			this.employeeRepository=employeeRepository;
			this.userRepository=userRepository;
			this.notifyRepository=notifyRepository;
		}
		
		
		protected override IQueryable<NotifyDto> QueryMain() 
		{
			var notifyQueryable=notifyRepository.Queryable;
			 var employeeQueryable = employeeRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _notify in notifyQueryable 
						join _publush_Id in employeeQueryable.TagWith("_publush_Id") on _notify.Publush_Id equals _publush_Id.Id into t__publush_Id
						from _publush_Id in t__publush_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.TagWith("_create_User_Id") on _notify.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.TagWith("_modify_User_Id") on _notify.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						 select new NotifyDto
						{
							Title=_notify.Title,
							Content=_notify.Content,
							Publush_Id=_notify.Publush_Id,
							Id=_notify.Id,
							Create_User_Id=_notify.Create_User_Id,
							CreateTime=_notify.CreateTime,
							Modify_User_Id=_notify.Modify_User_Id,
							ModifyTime=_notify.ModifyTime,
							Publush=_publush_Id==null?null:new EmployeeDto
							{
								Id = _publush_Id.Id,
								EnCode = _publush_Id.EnCode,
								Name = _publush_Id.Name,
								Email = _publush_Id.Email,
								PhoneNumber = _publush_Id.PhoneNumber,
								Gender = _publush_Id.Gender,
								User_Id = _publush_Id.User_Id,
								User = null,
								Dept_Id = _publush_Id.Dept_Id,
								Dept = null,
								Create_User_Id = _publush_Id.Create_User_Id,
								Create_User = null,
								CreateTime = _publush_Id.CreateTime,
								Modify_User_Id = _publush_Id.Modify_User_Id,
								Modify_User = null,
								ModifyTime = _publush_Id.ModifyTime,
							},
							Create_User=_create_User_Id==null?null:new UserDto
							{
								Id = _create_User_Id.Id,
								Account = _create_User_Id.Account,
								Password = _create_User_Id.Password,
								Name = _create_User_Id.Name,
								Email = _create_User_Id.Email,
								PhoneNumber = _create_User_Id.PhoneNumber,
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
