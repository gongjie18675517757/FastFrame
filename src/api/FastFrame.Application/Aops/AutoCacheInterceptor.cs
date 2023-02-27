using AspectCore.DynamicProxy;
using FastFrame.Infrastructure.Lock;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Threading.Tasks;


namespace FastFrame.Application
{
    /// <summary>
    /// 自动缓存
    /// </summary>
    public sealed class AutoCacheInterceptor : AbstractInterceptorAttribute
    {
//        public override async Task Invoke(AspectContext context, AspectDelegate next)
//        {
//            var parameterInfos = context
//                .ServiceMethod
//                .GetParameters()
//                .Select((v, i) => new {
//                    index = i,
//                    has_pars = v.GetCustomAttribute<AutoCacheParameterAttribute>() != null
//                })
//                .Where(v => v.has_pars)
//                .Select(v => context.Parameters[v.index]?.ToString());

//            var sb = new string[] {
//                    context.ServiceMethod.DeclaringType.Name,
//                    context.ServiceMethod.Name
//                }
//                .Concat(parameterInfos)
//                .Where(v => !string.IsNullOrWhiteSpace(v));

//            /*拼类名+方法名+参数*/
//            var resource = string.Join(".", sb);

//            var logger = context.ServiceProvider.GetService<ILogger<AutoCacheInterceptor>>();
//#if DEBUG
//            logger.LogDebug($"进入Lock:{resource}");
//#endif

//            /*尝试获取锁*/
//            var lockHolder = await context
//                .ServiceProvider
//                .GetService<ILockFacatory>()
//                .TryCreateLockAsync(resource, default);

//            if (lockHolder != null)
//            {
//                try
//                {
//                    await next(context);
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//                finally
//                {
//                    lockHolder.Dispose();
//                }
//            }
//            else
//            {
//#if DEBUG
//                logger.LogDebug($"未持有锁:{resource}");
//#endif
//            }

//            if (context.IsAsync())
//            {
//                context.ReturnValue = Task.CompletedTask;
//            }

//#if DEBUG
//            logger.LogDebug($"离开Lock:{resource}");
//#endif
//        }


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


    [System.AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public sealed class AutoCacheParameterAttribute : Attribute
    {

    }
}
