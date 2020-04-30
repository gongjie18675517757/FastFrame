using AspectCore.DynamicProxy;
using FastFrame.Dto;
using FastFrame.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    /// <summary>
    /// 自动缓存
    /// </summary>
    public class AutoCacheInterceptor : AbstractInterceptorAttribute
    {
        private readonly AutoCacheOperate autoCacheOperate;

        public AutoCacheInterceptor(AutoCacheOperate autoCacheOperate)
        {
            this.autoCacheOperate = autoCacheOperate;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);
            //var resultType = context.ProxyMethod.ReturnType;
            //if (resultType.IsGenericType)
            //    resultType = resultType.GetGenericArguments()[0];
            //switch (autoCacheOperate)
            //{
            //    case AutoCacheOperate.Get:
            //        {
            //            var pars = context.Parameters;
            //            var key = pars[0].ToString();
            //            var value = await RedisHelper.GetAsync(key);
            //            if (value.IsNullOrWhiteSpace())
            //            {
            //                await next(context);
            //                var result = await context.UnwrapAsyncReturnValue();
            //                var jsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            //                await RedisHelper.SetAsync(((IDto)result).Id, jsonValue, 60 * 60 * 10);
            //            }
            //            else
            //            {
            //                var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject(value, resultType);
            //                dynamic member = context.ServiceMethod.ReturnType.GetMember("Result")[0];
            //                dynamic temp = System.Convert.ChangeType(resultValue, member.PropertyType);
            //                context.ReturnValue = System.Convert.ChangeType(Task.FromResult(temp), context.ServiceMethod.ReturnType);
            //                //context.ReturnValue = Task.FromResult(resultValue);
            //            }
            //        }
            //        break;
            //    case AutoCacheOperate.Add:
            //        {
            //            await next(context);
            //            var result = await context.UnwrapAsyncReturnValue();
            //            var jsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            //            await RedisHelper.SetAsync(((IDto)result).Id, jsonValue, 60 * 60 * 10);
            //        }
            //        break;
            //    case AutoCacheOperate.Update:
            //        {
            //            await next(context);
            //            var result = await context.UnwrapAsyncReturnValue();
            //            var jsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            //            await RedisHelper.SetAsync(((IDto)result).Id, jsonValue, 60 * 60 * 10);
            //        }
            //        break;
            //    case AutoCacheOperate.Remove:
            //        {
            //            await next(context);
            //            var pars = (string[])context.Parameters[0];
            //            await RedisHelper.DelAsync(pars);
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
