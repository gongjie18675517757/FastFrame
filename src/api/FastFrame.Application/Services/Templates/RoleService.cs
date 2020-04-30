namespace FastFrame.Application.Services.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	/// <summary>
	///角色 服务实现 
	/// </summary>
	public partial class RoleService:BaseService<Role, RoleDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Role> roleRepository;
		
		public RoleService(IRepository<User> userRepository,IRepository<Role> roleRepository)
			:base(roleRepository)
		{
			this.userRepository=userRepository;
			this.roleRepository=roleRepository;
		}
		
		
		protected override IQueryable<RoleDto> QueryMain() 
		{
			var userQueryable = userRepository.Queryable;
			var query = from _role in roleRepository 
						join _create_User_Id in userQueryable on _role.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _role.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						let Create_User=new UserViewModel {Name=_create_User_Id.Name,Account=_create_User_Id.Account,Id=_create_User_Id.Id}
						let Modify_User=new UserViewModel {Name=_modify_User_Id.Name,Account=_modify_User_Id.Account,Id=_modify_User_Id.Id}
						 select new RoleDto
						{
							EnCode=_role.EnCode,
							Name=_role.Name,
							IsDefault=_role.IsDefault,
							IsAdmin=_role.IsAdmin,
							Remarks=_role.Remarks,
							Id=_role.Id,
							Create_User_Id=_role.Create_User_Id,
							CreateTime=_role.CreateTime,
							Modify_User_Id=_role.Modify_User_Id,
							ModifyTime=_role.ModifyTime,
							Create_User=Create_User,
							Modify_User=Modify_User,
					};
			return query;
		}
		public  Task<PageList<RoleViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = from _role in roleRepository 
						select new RoleViewModel
						{
							Name = _role.Name,
							EnCode = _role.EnCode,
							Id = _role.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
