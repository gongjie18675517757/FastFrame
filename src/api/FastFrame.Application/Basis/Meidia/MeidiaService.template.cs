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
		
		public MeidiaService(IRepository<Meidia> meidiaRepository,IRepository<User> userRepository)
			 : base(meidiaRepository)
		{
			this.meidiaRepository=meidiaRepository;
			this.userRepository=userRepository;
		}
		
		protected override IQueryable<MeidiaDto> QueryMain() 
		{
			var meidiaQueryable = meidiaRepository.Queryable.MapTo<Meidia,MeidiaViewModel>();
			var userQueryable = userRepository.Queryable.MapTo<User,UserViewModel>();
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
							TreeCode = _meidia.TreeCode,
							Id = _meidia.Id,
							Create_User_Id = _meidia.Create_User_Id,
							CreateTime = _meidia.CreateTime,
							Modify_User_Id = _meidia.Modify_User_Id,
							ModifyTime = _meidia.ModifyTime,
							Super = _super_Id,
							Create_User = _create_User_Id,
							Modify_User = _modify_User_Id,
							ChildCount = repository.Count(c => c.Super_Id == _meidia.Id)
						};
			return query;
		}
		public Task<IPageList<MeidiaViewModel>> ViewModelListAsync(IPagination page) 
		{
			var query = meidiaRepository.MapTo<Meidia, MeidiaViewModel>();
			return query.PageListAsync(page);
		}
		
	}
}
