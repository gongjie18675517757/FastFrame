using FastFrame.Application.Basis;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    /// <summary>
    /// 媒体库
    /// </summary>
    public partial class MeidiaController(MeidiaService service) : BaseController
    {
        [Permission(nameof(List), "列表")]
        [HttpGet("{id?}")]
        public async Task<MeidiaOutput> List(string id, string kw = "")
        {
            return await service.Meidias(id, kw);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Permission(nameof(Delete), "删除")]
        public virtual async Task Delete(string id)
        {
            await service.DeleteAsync(id);
        }

        /// <summary>
        /// 修改名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Permission("Update", "修改")]
        public async Task Put(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("名称不可为空", nameof(name));
            }

            await service.ReName(id, name);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Permission("Add", "添加")]
        [HttpPost]
        public virtual async Task<MeidiaModel> Post([FromBody]MeidiaDto @input)
        {
            var id = await service.AddAsync(@input);
            return await service.GetMeidia(id);
        }
    }
}
