namespace FastFrame.Service.Services.Chat
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Entity.Chat; 
	using FastFrame.Dto.Chat; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
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
						join _publush_Id in employeeQueryable.MapTo<Employee,EmployeeDto>() on _notify.Publush_Id equals _publush_Id.Id into t__publush_Id
						from _publush_Id in t__publush_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _notify.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _notify.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
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
						Publush=_publush_Id,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
