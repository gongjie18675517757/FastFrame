using FastFrame.Application;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers
{
    public abstract class BaseCURDController<TDto> : BaseController<TDto>
         where TDto : class, IDto, new()
    {
        private readonly ICURDService<TDto> service;

        public BaseCURDController(ICURDService<TDto> service) : base(service)
        {
            this.service = service;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Permission("Delete", "删除")]
        public virtual async Task Delete(string id)
        {
            await service.DeleteAsync(id);
        }





        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Permission("Add", "添加")]
        [HttpPost]
        public virtual async Task<string> Post([FromBody]TDto @input)
        {
            return await service.AddAsync(@input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Permission("Update", "修改")]
        public virtual async Task Put([FromBody]TDto @input)
        {
            await service.UpdateAsync(@input);
        }


        /// <summary>
        /// 验证唯一性
        /// </summary>      
        [HttpPost]
        [Permission(new string[] { "Add", "Update" })]
        public async Task<bool> VerififyUnique(UniqueInput uniqueInput)
        {
            return await service.VerifyUnique(uniqueInput);
        }
    }
}
