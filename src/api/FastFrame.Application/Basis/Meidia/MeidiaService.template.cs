	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
namespace FastFrame.Application.Basis
{
		
	/// <summary>
	/// 图片库 服务实现 
	/// </summary>
	public partial class MeidiaService:BaseService<Meidia, MeidiaDto>
	{
		private readonly IRepository<Meidia> meidiaRepository;
		private readonly IRepository<User> userRepository;
		
		public MeidiaService(IRepository<Meidia> meidiaRepository,IRepository<User> userRepository,IServiceProvider loader)
			 : base(loader,meidiaRepository)
		{
			this.meidiaRepository=meidiaRepository;
			this.userRepository=userRepository;
		}
		
		protected override IQueryable<MeidiaDto> DefaultQueryable() 
		{
			var meidiaQueryable = meidiaRepository.Queryable.Select(Meidia.BuildExpression());
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var repository = meidiaRepository.Queryable;
			var query = from _meidia in repository 
						join _super_Id in meidiaQueryable on _meidia.Super_Id equals _super_Id.Id into t__super_Id
						from _super_Id in t__super_Id.DefaultIfEmpty()
						join _create_User_Id in userQueryable on _meidia.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _meidia.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new MeidiaDto
						{
							Super_Id = _meidia.Super_Id,
							Name = _meidia.Name,
							Resource_Id = _meidia.Resource_Id,
							IsFolder = _meidia.IsFolder,
							Id = _meidia.Id,
							Create_User_Id = _meidia.Create_User_Id,
							CreateTime = _meidia.CreateTime,
							Modify_User_Id = _meidia.Modify_User_Id,
							ModifyTime = _meidia.ModifyTime,
							Super_Value = _super_Id.Value,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		protected override IQueryable<Entity.IViewModel> DefaultViewModelQueryable() 
		{
			return repository.Select(Meidia.BuildExpression());
		}
		protected override IQueryable<ITreeModel> DefaultTreeModelQueryable(string kw) 
		{
			        var main_query = repository.Queryable;
        if (!kw.IsNullOrWhiteSpace())
        {
            var vm_query = repository.Select(Meidia.BuildExpression());
            main_query = main_query
                .Where(a => 
                         vm_query.Any(v => 
                            v.Value.Contains(kw) &&
                            repository.Any(x => x.Id == v.Id && x.TreeCode.StartsWith(a.TreeCode))));
        }

        return from a in main_query
               join b in repository.Select(Meidia.BuildExpression()) on a.Id equals b.Id 
               select new TreeModel
               {
                   Id = a.Id,
                   Super_Id = a.Super_Id,
                   Value = b.Value,
                   ChildCount = main_query.Count(v => v.Super_Id == a.Id),
                   TotalChildCount = main_query.Count(v => v.Id != a.Id && v.TreeCode.StartsWith(a.TreeCode)),
               };
		}
		
	}
}
