﻿using FastFrame.Application.Chat;
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

        public DefaultController(IApplicationSession appSession, ILogger<DefaultController> logger)
        { 
            this.appSession = appSession;
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

        //// POST: api/Default
        //[HttpPost]
        //public async Task Post([FromBody] value)
        //{
        //    var userid = appSession.CurrUser?.Id;
        //    await messageBus.PubLishAsync(value);
        //}

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
