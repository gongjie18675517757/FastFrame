using FastFrame.Application.Privder;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers
{
    public class CommonController : BaseController
    {
        private readonly IScopeServiceLoader scopeServiceLoader;
        private readonly ITypeProvider typeProvider;

        public CommonController(IScopeServiceLoader scopeServiceLoader, ITypeProvider typeProvider)
        {
            this.scopeServiceLoader = scopeServiceLoader;
            this.typeProvider = typeProvider;
        }

        [HttpGet]
        public IEnumerable<CurrUser> Get()
        {
            return Enumerable.Range(1, 100).Select(x => new CurrUser());
        }

        /// <summary>
        /// 验证唯一性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> VerificationUnique(string id, [FromQuery]string moduleName, [FromQuery]string propName, [FromQuery]string value)
        {
            var entityType = typeProvider.GetTypeByName(moduleName);
            var areaName = T4Help.GenerateNameSpace(entityType, "");

            var serviceType = Type.GetType($"FastFrame.Service.{areaName}.{entityType.Name}Service");
            if (serviceType == null)
                throw new Exception("模块名称传入不正确!");

            var service = (IService)scopeServiceLoader.GetService(serviceType);
            return await service.VerifyUnique(id, propName, value);
        }
    }
}
