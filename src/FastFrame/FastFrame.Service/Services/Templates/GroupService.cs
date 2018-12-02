namespace FastFrame.Service.Services.Chat
{
	using FastFrame.Entity.Chat; 
	using FastFrame.Dto.Chat; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using FastFrame.Entity.Basis; 
	using System.Linq; 
	/// <summary>
	///群组 服务类 
	/// </summary>
	public partial class GroupService:BaseService<Group, GroupDto>
	{
		#region 字段
		private readonly IRepository<Foreign> foreignRepository;
		private readonly IRepository<User> userRepository;
		private readonly IRepository<Group> groupRepository;
		#endregion
		#region 构造函数
		public GroupService(IRepository<Foreign> foreignRepository,IRepository<User> userRepository,IRepository<Group> groupRepository,IScopeServiceLoader loader)
			:base(groupRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.groupRepository=groupRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<GroupDto> QueryMain() 
		{
			var groupQueryable=groupRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var query = from _group in groupQueryable 
					join foreing in foreignQueryable on _group.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new GroupDto
					{
						Name=_group.Name,
						LordUser_Id=_group.LordUser_Id,
						HandIcon_Id=_group.HandIcon_Id,
						Summary=_group.Summary,
						Id=_group.Id,
						Foreign = foreing,
						Create_User = user2,
						Modify_User = user3,
					};
			return query;
		}
		#endregion
	}
}
