namespace FastFrame.Service.Services.Basis
{
	using FastFrame.Repository.Basis; 
	using FastFrame.Entity.Basis; 
	using FastFrame.Dto.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using System.Linq; 
	/// <summary>
	///查询方案 服务类 
	/// </summary>
	public partial class QueryProgramService:BaseService<QueryProgram, QueryProgramDto>
	{
		#region 字段
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly QueryProgramRepository queryProgramRepository;
		#endregion
		#region 构造函数
		public QueryProgramService(ForeignRepository foreignRepository,UserRepository userRepository,QueryProgramRepository queryProgramRepository,IScopeServiceLoader loader)
			:base(queryProgramRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.queryProgramRepository=queryProgramRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<QueryProgramDto> QueryMain() 
		{
			var queryProgramQueryable=queryProgramRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var query = from queryProgram in queryProgramQueryable 
					join foreing in foreignQueryable on queryProgram.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new QueryProgramDto
					{
						ModuleName=queryProgram.ModuleName,
						Name=queryProgram.Name,
						IsPublic=queryProgram.IsPublic,
						Id=queryProgram.Id,
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
