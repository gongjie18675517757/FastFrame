using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers.CMS
{
    public partial class ArticleController
    {
        [Permission("ToggleRelease", "发布文章")]
        [HttpPut("{id}")]
        public async Task ToggleRelease(string id)
        {
            await service.ToggleRelease(id);
        }
    }
}
