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

namespace FastFrame.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IMessageBus messageBus;
        private readonly ICurrentUserProvider currentUserProvider;

        public DefaultController(IMessageBus messageBus, ICurrentUserProvider currentUserProvider)
        {
            this.messageBus = messageBus;
            this.currentUserProvider = currentUserProvider;
        }
        // GET: api/Default
        [HttpGet]
        public IEnumerable<string> Get()
        {
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
        public async Task Post([FromBody] Message value)
        {
            var userid = currentUserProvider.GetCurrUser().Id;
            await messageBus.PubLishAsync(new Message()
            {
                Category = MessageType.Notify,
                Content = value.Content,
                FromUserIds = new string[] { },
                ToUserIds = new[] { userid }
            });          
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
