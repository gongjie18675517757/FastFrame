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
	using System.Threading.Tasks; 
	using FastFrame.Dto.Basis; 
	/// <summary>
	///群组 服务实现 
	/// </summary>
	public partial class GroupService:BaseService<Group, GroupDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Group> groupRepository;
		
		public GroupService(IRepository<User> userRepository,IRepository<Group> groupRepository)
			:base(groupRepository)
		{
			this.userRepository=userRepository;
			this.groupRepository=groupRepository;
		}
		
		
		protected override IQueryable<GroupDto> QueryMain() 
		{
			var userQueryable = userRepository.Queryable;
			var query = from _group in groupRepository 
						join _create_User_Id in userQueryable on _group.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _group.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						let Create_User=new UserViewModel {Name=_create_User_Id.Name,Account=_create_User_Id.Account,Id=_create_User_Id.Id}
						let Modify_User=new UserViewModel {Name=_modify_User_Id.Name,Account=_modify_User_Id.Account,Id=_modify_User_Id.Id}
						 select new GroupDto
						{
							Name=_group.Name,
							LordUser_Id=_group.LordUser_Id,
							HandIcon_Id=_group.HandIcon_Id,
							Summary=_group.Summary,
							Id=_group.Id,
							Create_User_Id=_group.Create_User_Id,
							CreateTime=_group.CreateTime,
							Modify_User_Id=_group.Modify_User_Id,
							ModifyTime=_group.ModifyTime,
							Create_User=Create_User,
							Modify_User=Modify_User,
					};
			return query;
		}
		public  Task<PageList<GroupViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = from _group in groupRepository 
						select new GroupViewModel
						{
							Name = _group.Name,
							LordUser_Id = _group.LordUser_Id,
							HandIcon_Id = _group.HandIcon_Id,
							Summary = _group.Summary,
							Id = _group.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
