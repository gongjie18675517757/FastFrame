namespace FastFrame.Service.Services.CMS
{
    using FastFrame.Repository.CMS;
    using FastFrame.Entity.CMS;
    using FastFrame.Repository.Basis;
    using FastFrame.Entity.Basis;
    using FastFrame.Dto.CMS;
    using FastFrame.Infrastructure.Interface;
    using FastFrame.Infrastructure;
    using System.Linq;
    using FastFrame.Dto.Basis;
    using System;

    /// <summary>
    ///图片库 服务类 
    /// </summary>
    public partial class MeidiaService:BaseService<Meidia, MeidiaDto>
	{
		#region 字段
		private readonly MeidiaRepository meidiaRepository;  
        private readonly ResourceRepository resourceRepository;
		private readonly ForeignRepository foreignRepository;
		private readonly UserRepository userRepository;
		#endregion
		#region 构造函数
		public MeidiaService(MeidiaRepository meidiaRepository,ResourceRepository resourceRepository,ForeignRepository foreignRepository,UserRepository userRepository,IScopeServiceLoader loader)
			:base(meidiaRepository,loader)
		{
			this.meidiaRepository=meidiaRepository;
			this.resourceRepository=resourceRepository;
			this.foreignRepository=foreignRepository;
			this.userRepository=userRepository;
		}
		#endregion
		#region 属性
		#endregion
		#region 方法
		protected override IQueryable<MeidiaDto> QueryMain() 
		{
			var foreignQueryable = foreignRepository.Queryable;
			var userQuerable = userRepository.Queryable;
			 var meidiaQueryable = meidiaRepository.Queryable;
			 var resourceQueryable = resourceRepository.Queryable;
			 var query = from meidia in meidiaQueryable 
						join parent_Id in meidiaQueryable.MapTo<Meidia,MeidiaDto>() on meidia.Parent_Id equals parent_Id.Id into t_parent_Id
						from parent_Id in t_parent_Id.DefaultIfEmpty()
						join resource_Id in resourceQueryable.MapTo<Resource,ResourceDto>() on meidia.Resource_Id equals resource_Id.Id into t_resource_Id
						from resource_Id in t_resource_Id.DefaultIfEmpty()
					join foreing in foreignQueryable on meidia.Id equals foreing.EntityId into t_foreing
					from foreing in t_foreing.DefaultIfEmpty()
					join user2 in userQuerable on foreing.CreateUserId equals user2.Id into t_user2
					from user2 in t_user2.DefaultIfEmpty()
					join user3 in userQuerable on foreing.ModifyUserId equals user3.Id into t_user3
					from user3 in t_user3.DefaultIfEmpty()
					 select new MeidiaDto
					{
						Parent_Id=meidia.Parent_Id,
						Href=meidia.Href,
						Name=meidia.Name,
						Resource_Id=meidia.Resource_Id,
						IsFolder=meidia.IsFolder,
						Id=meidia.Id,
						Parent=parent_Id,
						Resource=resource_Id,
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
