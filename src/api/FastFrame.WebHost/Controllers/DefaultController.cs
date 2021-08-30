using FastFrame.Application.Chat;
using FastFrame.Infrastructure.Client;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IApplicationSession appSession;
        private readonly ILogger<DefaultController> logger;
        private readonly IClientManage clientManage;

        public DefaultController(IApplicationSession appSession, ILogger<DefaultController> logger, IClientManage clientManage)
        {
            this.appSession = appSession;
            this.logger = logger;
            this.clientManage = clientManage;
        }

       
        [HttpGet]
        public async Task<string[]> Get(string title, string content)
        {
            return await clientManage
                .PublishChooseAsync(new ClientChoose
                {
                    Text = content,
                    Title = title,
                    Values = new[] {
                                    new KeyValuePair<string,string>("1","1"),
                                    new KeyValuePair<string,string>("2","2"),
                    },
                    Multiple = true,
                    Timeout = 10
                }, appSession.CurrUser?.Id);
        }

        // POST: api/Default
        [HttpPost]
        public async Task<bool> Post([FromForm] string title, [FromForm] string content)
        {
            return await clientManage
                .PublishConfirmAsync(new ClientConfirm
                {

                    Title = title,
                    Content = "是否确认?",
                    Timeout = 10
                }, appSession.CurrUser?.Id);
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }



}
