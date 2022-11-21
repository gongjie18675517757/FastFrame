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
							TreeCode = _meidia.TreeCode,
							Id = _meidia.Id,
							Create_User_Id = _meidia.Create_User_Id,
							CreateTime = _meidia.CreateTime,
							Modify_User_Id = _meidia.Modify_User_Id,
							ModifyTime = _meidia.ModifyTime,
							Super_Value = _super_Id.Value,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
							ChildCount = repository.Count(c => c.Super_Id == _meidia.Id)
						};
			return query;
		}
		
	}
}
