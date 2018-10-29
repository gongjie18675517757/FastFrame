namespace FastFrame.Service.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using System.Linq; 
	/// <summary>
	///查询方案明细 服务类 
	/// <summary>
	public partial class QueryProgramDetailService:BaseService<QueryProgramDetail, QueryProgramDetailDto>
	{
		#region 字段
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly QueryProgramDetailRepository queryProgramDetailRepository;
		#endregion
		#region 构造函数
		public QueryProgramDetailService(ForeignRepository foreignRepository,UserRepository userRepository,QueryProgramDetailRepository queryProgramDetailRepository,IScopeServiceLoader loader)
			:base(queryProgramDetailRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.queryProgramDetailRepository=queryProgramDetailRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<QueryProgramDetailDto> QueryMain() 
		{
			var queryProgramDetailQueryable=queryProgramDetailRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var query = from queryProgramDetail in queryProgramDetailQueryable 
					join foreing in foreignQueryable on queryProgramDetail.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new QueryProgramDetailDto
					{
						SearchProgram_Id=queryProgramDetail.SearchProgram_Id,
						Name=queryProgramDetail.Name,
						Compare=queryProgramDetail.Compare,
						Value=queryProgramDetail.Value,
						Id=queryProgramDetail.Id,
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
