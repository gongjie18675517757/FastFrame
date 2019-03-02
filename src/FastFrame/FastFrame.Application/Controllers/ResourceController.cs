using FastFrame.Dto.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service.Services.Basis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers
{
    /// <summary>
    /// 资源
    /// </summary>
    public class ResourceController : BaseController
    {
        private readonly IResourceProvider resourceProvider;
        private readonly ResourceService resourceService;

        public ResourceController(IResourceProvider resourceProvider, ResourceService resourceService)
        {
            this.resourceProvider = resourceProvider;
            this.resourceService = resourceService;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<ResourceDto>> Post(List<IFormFile> files)
        {
            if (Request.Form.Files.Count == 0)
                throw new System.Exception("无有效文件!");
            var result = new List<ResourceDto>();

            //var  files = Request.Form.Files;

            foreach (var formFile in files)
            {
                var stream = formFile.OpenReadStream();
                var path = await resourceProvider.SetResource(stream);
                result.Add(await resourceService.AddAsync(new ResourceDto()
                {
                    ContentType = formFile.ContentType,
                    Name = formFile.FileName,
                    Path = path,
                    Size = formFile.Length,
                    MD5 = stream.ToMD5()
                }));
            }

            return result;
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var resource = await resourceService.GetAsync(id);
            if (resource == null)
                throw new System.Exception("资源不存在");
            var stream = await resourceProvider.GetResource(resource.Path);
            return File(stream, resource.ContentType, resource.Name, true);
        }
    }
}
