using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRedis;
using FastFrame.Application.Hubs;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.MessageBus;
using Microsoft.Extensions.Logging;
using FastFrame.Dto.Dtos.Chat;

namespace FastFrame.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IMessageBus messageBus;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly ILogger<DefaultController> logger;

        public DefaultController(IMessageBus messageBus, ICurrentUserProvider currentUserProvider,ILogger<DefaultController> logger)
        {
            this.messageBus = messageBus;
            this.currentUserProvider = currentUserProvider;
            this.logger = logger;
        }
        // GET: api/Default
        [HttpGet]
        public IEnumerable<string> Get()
        {
            logger.LogError("xxxxxxx");
            return new string[] { "value1", "value2" };
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        [HttpPost]
        public async Task Post([FromBody] Message<RecMsgOutPut> value)
        {
            var userid = currentUserProvider.GetCurrUser().Id; 
            await messageBus.PubLishAsync(value);
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
