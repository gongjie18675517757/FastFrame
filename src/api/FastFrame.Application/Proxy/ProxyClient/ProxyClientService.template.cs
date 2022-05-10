using FastFrame.Entity.Basis;
using FastFrame.Entity.Proxy;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure;
using FastFrame.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FastFrame.Application.Basis;
namespace FastFrame.Application.Proxy
{

    /// <summary>
    /// 内网穿透服务 服务实现 
    /// </summary>
    public partial class ProxyClientService:BaseService<ProxyClient, ProxyClientDto>
	{
		private readonly IRepository<User> userRepository;
		private readonly IRepository<ProxyClient> proxyClientRepository;
		
		public ProxyClientService(IRepository<User> userRepository,IRepository<ProxyClient> proxyClientRepository)
			 : base(proxyClientRepository)
		{
			this.userRepository=userRepository;
			this.proxyClientRepository=proxyClientRepository;
		}
		
		protected override IQueryable<ProxyClientDto> QueryMain() 
		{
			var userQueryable = userRepository.Queryable.MapTo<User,UserViewModel>();
			var repository = proxyClientRepository.Queryable;
			var query = from _proxyClient in repository 
						join _create_User_Id in userQueryable on _proxyClient.Create_User_Id equals _create_User_Id.Id into t__create_User_Id
						from _create_User_Id in t__create_User_Id.DefaultIfEmpty()
						join _modify_User_Id in userQueryable on _proxyClient.Modify_User_Id equals _modify_User_Id.Id into t__modify_User_Id
						from _modify_User_Id in t__modify_User_Id.DefaultIfEmpty()
						select new ProxyClientDto
						{
							Name = _proxyClient.Name,
							ClientToken = _proxyClient.ClientToken,
							Description = _proxyClient.Description,
							Id = _proxyClient.Id,
							Create_User_Id = _proxyClient.Create_User_Id,
							CreateTime = _proxyClient.CreateTime,
							Modify_User_Id = _proxyClient.Modify_User_Id,
							ModifyTime = _proxyClient.ModifyTime,
							Create_User = _create_User_Id,
							Modify_User = _modify_User_Id,
						};
			return query;
		}
		public Task<IPageList<ProxyClientViewModel>> ViewModelListAsync(IPagination page) 
		{
			var query = proxyClientRepository.MapTo<ProxyClient, ProxyClientViewModel>();
			return query.PageListAsync(page);
		}
		
	}
}
