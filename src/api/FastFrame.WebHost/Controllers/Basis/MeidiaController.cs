using FastFrame.Dto.Basis;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Service.Services.Basis;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class MeidiaController : BaseController
    {
        private readonly MeidiaService service;

        public MeidiaController(MeidiaService service)
        {
            this.service = service;
        }

        [HttpGet("{id?}")]
        public async Task<MeidiaOutput> List(string id, string v = "")
        {
            return await service.Meidias(id, v);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
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
