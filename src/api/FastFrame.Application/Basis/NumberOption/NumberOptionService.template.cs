namespace FastFrame.Application.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	using System.Threading.Tasks; 
	using FastFrame.Application.Basis; 
		
	/// <summary>
	/// 编号设置 服务实现 
	/// </summary>
	public partial class NumberOptionService:BaseService<NumberOption, NumberOptionDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<NumberOption> numberOptionRepository;
		
		public NumberOptionService(IRepository<User> userRepository,IRepository<NumberOption> numberOptionRepository)
			 : base(numberOptionRepository)
		{
			this.userRepository=userRepository;
			this.numberOptionRepository=numberOptionRepository;
		}
		
		protected override IQueryable<NumberOptionDto> QueryMain() 
		{
			var userQueryable = userRepository.Queryable;
			var query = from _numberOption in numberOptionRepository 
						join _create_User_Id in userQueryable on _numberOption.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _numberOption.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						let Create_User = new UserViewModel {Name = _create_User_Id.Name,Account = _create_User_Id.Account,Id = _create_User_Id.Id}
						let Modify_User = new UserViewModel {Name = _modify_User_Id.Name,Account = _modify_User_Id.Account,Id = _modify_User_Id.Id}
						select new NumberOptionDto
						{
							BeModule=_numberOption.BeModule,
							Prefix=_numberOption.Prefix,
							TaskDate=_numberOption.TaskDate,
							SerialLength=_numberOption.SerialLength,
							Suffix=_numberOption.Suffix,
							DateField=_numberOption.DateField,
							DateFieldText=_numberOption.DateFieldText,
							FmtDate=_numberOption.FmtDate,
							Id=_numberOption.Id,
							Create_User_Id=_numberOption.Create_User_Id,
							CreateTime=_numberOption.CreateTime,
							Modify_User_Id=_numberOption.Modify_User_Id,
							ModifyTime=_numberOption.ModifyTime,
							Create_User=Create_User,
							Modify_User=Modify_User,
						};
			return query;
		}
		public  Task<PageList<NumberOptionViewModel>> ViewModelListAsync(Pagination page) 
		{
			var query = from _numberOption in numberOptionRepository 
						select new NumberOptionViewModel
						{
							BeModule = _numberOption.BeModule,
							Prefix = _numberOption.Prefix,
							TaskDate = _numberOption.TaskDate,
							SerialLength = _numberOption.SerialLength,
							Suffix = _numberOption.Suffix,
							DateField = _numberOption.DateField,
							DateFieldText = _numberOption.DateFieldText,
							FmtDate = _numberOption.FmtDate,
							Id = _numberOption.Id,
						};
			return query.PageListAsync(page);
		}
		
	}
}
