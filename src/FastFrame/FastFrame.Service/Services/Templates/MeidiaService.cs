namespace FastFrame.Service.Services.CMS
{
	using FastFrame.Repository.CMS; 
	using FastFrame.Entity.CMS; 
	using FastFrame.Dto.CMS; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using System.Linq;
    using FastFrame.Repository.Basis;

    /// <summary>
    ///图片库 服务类 
    /// </summary>
    public partial class MeidiaService:BaseService<Meidia, MeidiaDto>
	{
		#region 字段
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		private readonly MeidiaRepository meidiaRepository;
		#endregion
		#region 构造函数
		public MeidiaService(ForeignRepository foreignRepository,UserRepository userRepository,MeidiaRepository meidiaRepository,IScopeServiceLoader loader)
			:base(meidiaRepository,loader)
		{
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
			this.meidiaRepository=meidiaRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<MeidiaDto> QueryMain() 
		{
			var meidiaQueryable=meidiaRepository.Queryable;
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var query = from meidia in meidiaQueryable 
					join foreing in foreignQueryable on meidia.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new MeidiaDto
					{
						Href=meidia.Href,
						Name=meidia.Name,
						Id=meidia.Id,
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
