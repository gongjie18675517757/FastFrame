namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	/// <summary>
	///角色 服务类 
	/// </summary>
	public partial class RoleService:BaseService<Role, RoleDto>
	{
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Role> roleRepository;
		
		/*构造函数*/
		public RoleService(IRepository<User> userRepository,IRepository<Role> roleRepository,IScopeServiceLoader loader)
			:base(roleRepository,loader)
		{
			this.userRepository=userRepository;
			this.roleRepository=roleRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<RoleDto> QueryMain() 
		{
			var roleQueryable=roleRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _role in roleQueryable 
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _role.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _role.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
					 select new RoleDto
					{
						EnCode=_role.EnCode,
						Name=_role.Name,
						Id=_role.Id,
						Create_User_Id=_role.Create_User_Id,
						CreateTime=_role.CreateTime,
						Modify_User_Id=_role.Modify_User_Id,
						ModifyTime=_role.ModifyTime,
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
