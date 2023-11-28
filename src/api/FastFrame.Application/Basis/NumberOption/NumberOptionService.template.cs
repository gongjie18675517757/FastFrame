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
	/// 编号设置 服务实现 
	/// </summary>
	public partial class NumberOptionService(IRepository<User> userRepository,IRepository<NumberOption> numberOptionRepository,IServiceProvider loader):BaseService<NumberOption, NumberOptionDto>(loader,numberOptionRepository)
	{
		private readonly IRepository<User> userRepository=userRepository;
		private readonly IRepository<NumberOption> numberOptionRepository=numberOptionRepository;
		
		
		protected override IQueryable<NumberOptionDto> DefaultQueryable() 
		{
			var userQueryable = userRepository.Queryable.Select(User.BuildExpression());
			var repository = numberOptionRepository.Queryable;
			var query = from _numberOption in repository 
						join _create_User_Id in userQueryable on _numberOption.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _numberOption.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new NumberOptionDto
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
							Create_User_Id = _numberOption.Create_User_Id,
							CreateTime = _numberOption.CreateTime,
							Modify_User_Id = _numberOption.Modify_User_Id,
							ModifyTime = _numberOption.ModifyTime,
							Create_User_Value = _create_User_Id.Value,
							Modify_User_Value = _modify_User_Id.Value,
						};
			return query;
		}
		
	}
}
