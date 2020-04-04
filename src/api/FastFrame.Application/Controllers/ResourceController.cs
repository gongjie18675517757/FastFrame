using FastFrame.Dto.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service.Services.Basis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
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
        public async Task<IEnumerable<string>> Post()
        {
            if (Request.Form.Files.Count == 0)
                throw new System.Exception("无有效文件!");
            var result = new List<string>();

            var files = Request.Form.Files;

            foreach (var formFile in files)
            {
                var stream = formFile.OpenReadStream();
                stream.Position = 0;
                var md5 = stream.ToMD5();
                stream.Position = 0;

                var path = await resourceService.GetPathByMd5Async(md5);

                if (path.IsNullOrWhiteSpace())
                    path = await resourceProvider.WriteAsync(stream);

                result.Add(await resourceService.AddAsync(new ResourceDto()
                {
                    ContentType = formFile.ContentType,
                    Name = formFile.FileName,
                    Path = path,
                    Size = formFile.Length,
                    MD5 = md5,
                }));
            }

            return result;
        }

        /// <summary>
        /// 下载
        /// </summary> 
        [HttpGet("{id}/{name?}")]
        public async Task<IActionResult> Get(string id, string name)
        {
            var resource = await resourceService.GetAsync(id);
            if (resource == null)
                return NotFound();

            var stream = await resourceProvider.ReadAsync(resource.Path);
            if (stream == null)
                return NotFound();

            Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
            Response.Headers.Add("Last-Modified", DateTime.Now.ToString());
            Response.Headers.Add("ETag", id);

            if (resource.ContentType.StartsWith("image"))
            {
                var buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                return new FileContentResult(buffer, resource.ContentType);
            }
            else
            {
                return File(stream, resource.ContentType, name ?? resource.Name, true);
            }
        }
    }
}
