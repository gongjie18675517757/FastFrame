namespace FastFrame.Service.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using System.Linq; 
	/// <summary>
	///组织信息 服务类 
	/// <summary>
	public partial class OrganizeService:BaseService<Organize, OrganizeDto>
	{
		#region 字段
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly OrganizeRepository organizeRepository;
		#endregion
		#region 构造函数
		public OrganizeService(ForeignRepository foreignRepository,UserRepository userRepository,OrganizeRepository organizeRepository,IScopeServiceLoader loader)
			:base(organizeRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.organizeRepository=organizeRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<OrganizeDto> QueryMain() 
		{
			var organizeQueryable=organizeRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var query = from organize in organizeQueryable 
					join foreing in foreignQueryable on organize.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new OrganizeDto
					{
						Name=organize.Name,
						Id=organize.Id,
						CreateAccount = user2.Account,
						CreateName = user2.Name,
						CreateTime = foreing.CreateTime,
						ModifyAccount = user3.Account,
						ModifyName = user3.Name,
						ModifyTime = foreing.ModifyTime,
					};
			return query;
		}
		#endregion
	}
}
