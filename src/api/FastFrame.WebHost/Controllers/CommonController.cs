using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FastFrame.WebHost.Controllers
{
    [AllowAnonymous]
    public class CommonController : BaseController
    {
        private readonly IModuleExportProvider moduleExportProvider;

        public CommonController(IModuleExportProvider moduleExportProvider)
        {
            this.moduleExportProvider = moduleExportProvider;
        }

        /// <summary>
        /// 生成ID
        /// </summary> 
        [HttpGet] 
        public IEnumerable<string> Get(int count = 1)
        {
            return Enumerable.Range(1, count).Select(x => IdGenerate.NetId());
        }

        /// <summary>
        /// 生成密码
        /// </summary> 
        [HttpGet("{pwd}")] 
        public KeyValuePair<string, string> MakePassword(string pwd)
        {
            var user = new User();
            user.GeneratePassword(pwd);
            return new KeyValuePair<string, string>(user.EncryptionKey, user.Password);
        }

        /// <summary>
        /// 获取模块结构
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        [HttpGet("{typeName}")]
        public ModuleStruct ModuleStruts(string typeName)
        {
            return moduleExportProvider.GetModuleStruts(typeName);
        }

        /// <summary>
        /// 需要审核的模块
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<KeyValuePair<string, string>> HaveCheckModuleList()
        {
            return moduleExportProvider.HaveCheckModuleList();
        }
    }
}
