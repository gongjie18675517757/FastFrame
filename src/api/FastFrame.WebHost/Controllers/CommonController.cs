using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Module;
using FastFrame.Infrastructure.RSAOperate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace FastFrame.WebHost.Controllers
{
    [AllowAnonymous]
    public class CommonController : BaseController
    {
        private readonly IModuleExportProvider moduleExportProvider;
        private readonly RSAProvider rsaProvider;

        public CommonController(IModuleExportProvider moduleExportProvider, RSAProvider rsaProvider)
        {
            this.moduleExportProvider = moduleExportProvider;
            this.rsaProvider = rsaProvider;
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

        /// <summary>
        /// 需要编码的模块
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<KeyValuePair<string, string>> HaveNumberModuleList()
        {
            return moduleExportProvider.HaveNumberModuleList();
        }

        [AllowAnonymous]
        [HttpGet]
        public string GetPublicKey()
        {
            return rsaProvider.PublicKey;
        }


#if DEBUG
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> TestPaginationFromJson([FromForm] string input)
        {
            await Task.Yield();
            var pagination = Pagination<User>.FromJson(input);
            return Ok(pagination);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Test(string input)
        {
            await Task.CompletedTask;
            var users = Request.HttpContext.RequestServices.GetService<Database.DataBase>().Set<User>().Where(v => true);
            IQueryable<string> filter = Request.HttpContext.RequestServices.GetService<Database.DataBase>().Set<DeptMember>().Where(v => true).Select(v => v.User_Id);


            //var field_experssion = ExpressionClosureFactory.ParseLambda<User, string>("Id");

            //var method = typeof(Queryable)
            //    .GetMethods()
            //    .Where(v => v.Name == "Contains")
            //    .Where(v => v.GetParameters().Length == 2)
            //    .FirstOrDefault()
            //    ?.MakeGenericMethod(typeof(string));

            //var left = ExpressionClosureFactory.GetField(filter);
            //var methodCallExpression = Expression.Call(method, left, field_experssion.Body);

            //var lambdaExpression = Expression.Lambda<Func<User, bool>>(methodCallExpression, field_experssion.Parameters);
            //var x = await users.Where(lambdaExpression).ToListAsync();

            var xx = ExpressionClosureFactory.BuildContainsExpression<User, string>("Id", filter);
            var x = await users.Where(xx).ToListAsync();
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Test2(string input)
        {
            await Task.CompletedTask;
            var users = Request.HttpContext.RequestServices.GetService<Database.DataBase>().Set<User>().Where(v => true);
            IQueryable<DeptMember> filter = Request.HttpContext.RequestServices.GetService<Database.DataBase>().Set<DeptMember>().Where(v => true);


            //Expression<Func<User, bool>> expression = v => filter.Any(x => x.User_Id == v.Id);

            //var user_field_experssion = ExpressionClosureFactory.ParseLambda<User, string>("Id");
            //var dept_member_field_experssion = ExpressionClosureFactory.ParseLambda<DeptMember, string>(v=>v.User_Id);

            //var method = typeof(Queryable)
            //    .GetMethods()
            //    .Where(v => v.Name == "Any")
            //    .Where(v => v.GetParameters().Length == 2)
            //    .FirstOrDefault()
            //    ?.MakeGenericMethod(typeof(DeptMember));

            //var equalExpression = Expression.Equal(user_field_experssion.Body, dept_member_field_experssion.Body);
            //var equalExpression2 = Expression.Lambda<Func<DeptMember, bool>>(equalExpression, dept_member_field_experssion.Parameters);

            //var left = ExpressionClosureFactory.GetField(filter);
            //var methodCallExpression = Expression.Call(method, left, equalExpression2);

            //var lambdaExpression = Expression.Lambda<Func<User, bool>>(methodCallExpression, user_field_experssion.Parameters);
            //var x = await users.Where(lambdaExpression).ToListAsync();




            var lambdaExpression = ExpressionClosureFactory.BuildAnyExpression<User, DeptMember, string>(v => v.Id, v => v.User_Id, filter);
            var lambdaExpression2 = ExpressionClosureFactory.BuildAnyExpression<User, DeptMember, string>("Id", "User_Id", filter);
            var x = await users.Where(lambdaExpression).ToListAsync();
            var x2 = await users.Where(lambdaExpression2).ToListAsync();


            return Ok();
        }
#endif
    }
}
