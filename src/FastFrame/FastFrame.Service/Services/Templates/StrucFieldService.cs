namespace FastFrame.Service.Services.Module
{
	using FastFrame.Entity.Module; 
	using FastFrame.Dto.Module; 
	using FastFrame.Infrastructure.Interface; 
	using FastFrame.Infrastructure; 
	using FastFrame.Repository; 
	using FastFrame.Entity.Basis; 
	using System.Linq; 
	using Microsoft.EntityFrameworkCore; 
	/// <summary>
	///字段 服务类 
	/// </summary>
	public partial class StrucFieldService:BaseService<StrucField, StrucFieldDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<StrucField> strucFieldRepository;
		
		public StrucFieldService(IRepository<User> userRepository,IRepository<StrucField> strucFieldRepository,IScopeServiceLoader loader)
			:base(strucFieldRepository,loader)
		{
			this.userRepository=userRepository;
			this.strucFieldRepository=strucFieldRepository;
		}
		
		
		protected override IQueryable<StrucFieldDto> QueryMain() 
		{
			var strucFieldQueryable=strucFieldRepository.Queryable;
			 var query = from _strucField in strucFieldQueryable 
						 select new StrucFieldDto
						{
							Name=_strucField.Name,
							TypeName=_strucField.TypeName,
							Description=_strucField.Description,
							Hide=_strucField.Hide,
							Readonly=_strucField.Readonly,
							DefaultValue=_strucField.DefaultValue,
							Relate_Id=_strucField.Relate_Id,
							IsTextArea=_strucField.IsTextArea,
							IsRichText=_strucField.IsRichText,
							IsRequired=_strucField.IsRequired,
							Struct_Id=_strucField.Struct_Id,
							Id=_strucField.Id,
					};
			return query;
		}
		
	}
}
