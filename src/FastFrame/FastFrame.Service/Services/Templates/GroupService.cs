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
	///群组 服务类 
	/// </summary>
	public partial class GroupService:BaseService<Group, GroupDto>
	{
		/*字段*/
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Group> groupRepository;
		
		/*构造函数*/
		public GroupService(IRepository<User> userRepository,IRepository<Group> groupRepository,IScopeServiceLoader loader)
			:base(groupRepository,loader)
		{
			this.userRepository=userRepository;
			this.groupRepository=groupRepository;
		}
		
		/*属性*/
		
		/*方法*/
		protected override IQueryable<GroupDto> QueryMain() 
		{
			var groupQueryable=groupRepository.Queryable;
			 var userQueryable = userRepository.Queryable;
			 var query = from _group in groupQueryable 
						join _create_User_Id in userQueryable.MapTo<User,UserDto>() on _group.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable.MapTo<User,UserDto>() on _group.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
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
						Create_User=_create_User_Id,
						Modify_User=_modify_User_Id,
					};
			return query;
		}
		
	}
}
