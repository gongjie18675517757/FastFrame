using FastFrame.Dto.Basis;
using FastFrame.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class NotifyController
    {
        /// <summary>
        /// 所有通知
        /// </summary> 
        [HttpPost]
        public async Task<PageList<NotifyDto>> AllList([FromBody]PagePara pageInfo)
        {
            return await service.GetListAsync(pageInfo);
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
    }
}
