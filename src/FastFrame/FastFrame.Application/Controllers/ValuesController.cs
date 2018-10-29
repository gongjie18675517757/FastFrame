using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace FastFrame.Application.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ValuesController : Controller
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IHubContext<Hubs.ChatHub> hubContext;

        public ValuesController(IServiceProvider serviceProvider, IHubContext<Hubs.ChatHub> hubContext)
        {
            this.serviceProvider = serviceProvider;
            this.hubContext = hubContext;
        }

        [HttpGet]
        public async Task<User> GetTest()
        {
            var currentUserProvider = serviceProvider.GetService<ICurrentUserProvider>();

            var u = new User();
            u.Account = currentUserProvider.GetCurrOrganizeId();
            return await Task.FromResult(u);
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetList()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet]
        public ActionResult<string> Get(int id = 0)
        {
            return id.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


}
