using AspectCore.DynamicProxy;
using FastFrame.Infrastructure.Lock;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using FastFrame.Infrastructure.Interface;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using AspectCore.Configuration;
using AspectCore.DependencyInjection;

namespace FastFrame.Application
{
    /// <summary>
    /// 方法锁，同一时间只允许一个相同参数的实例执行
    /// 要求无参或简单参数
    /// 要求返回Task,或者void
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class LockMethodAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var parameterInfos = context
                .ServiceMethod
                .GetParameters()
                .Select((v,i)=>new { 
                    index=i,
                    has_pars= v.GetCustomAttribute<LockMethodParameterAttribute>() != null
                })
                .Where(v=>v.has_pars)
                .Select(v=> context.Parameters[v.index]?.ToString());

            var sb = new string[] {
                    context.ServiceMethod.DeclaringType.Name,
                    context.ServiceMethod.Name
                }
                .Concat(parameterInfos)
                .Where(v => !string.IsNullOrWhiteSpace(v));

            /*拼类名+方法名+参数*/
            var resource = string.Join(".", sb);

            var logger = context.ServiceProvider.GetService<ILogger<LockMethodAttribute>>();
#if DEBUG
            logger.LogDebug($"进入Lock:{resource}");
#endif

            /*尝试获取锁*/
            var lockHolder = await context
                .ServiceProvider
                .GetService<ILockFacatory>()
                .TryCreateLockAsync(resource,default);

            if (lockHolder != null)
            {
                try
                {
                    await next(context);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    lockHolder.Dispose();
                }
            }
            else
            {
#if DEBUG
                logger.LogDebug($"未持有锁:{resource}");
#endif
            }

            if (context.IsAsync())
            {
                context.ReturnValue = Task.CompletedTask;
            }

#if DEBUG
            logger.LogDebug($"离开Lock:{resource}");
#endif
        }

    }

    /// <summary>
    /// 方法锁参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public sealed class LockMethodParameterAttribute : Attribute
    {
         
    }

    public class TestLockMethodService : IService
    {
       

        [LockMethod]
        public virtual async Task TestInvoke()
        {
      
            await Task.Delay(1000);
        }
    } 
}
