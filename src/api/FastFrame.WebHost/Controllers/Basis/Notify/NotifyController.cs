using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Infrastructure.Permission;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class NotifyController
    {
        /// <summary>
        /// 所有通知
        /// </summary> 
        [HttpGet]
        public async Task<PageList<NotifyDto>> AllList(string qs)
        {
            return await service.PageListAsync(qs.ToObject<Pagination>(true));
        }

        /// <summary>
        /// 通知内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<NotifyDto> GetNotify(string id)
        {
            return await service.GetAsync(id);
        }


        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<PageList<UserViewModel>> UserList(string qs)
            => Request.HttpContext.RequestServices
                    .GetService<UserService>().ViewModelListAsync(qs.ToObject<Pagination>(true));
    }
}
