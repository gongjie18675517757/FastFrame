﻿using AspectCore.DynamicProxy;
using FastFrame.Infrastructure.Lock;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            /*此处判断当前是否有在执行*/
            //context.ServiceMethod.Name
            //"ExistsNotInitAsync"
            //context.ServiceMethod.DeclaringType.Name
            //"HanldeTreeService"
            //context.Parameters
            //{ object[0]}

            var sb = new string[] {
                    context.ServiceMethod.DeclaringType.Name,
                    context.ServiceMethod.Name
                }
                .Concat(context.Parameters.Select(v => v?.ToString()))
                .Where(v => !string.IsNullOrWhiteSpace(v));

            var resource = string.Join(".", sb);

            var lockHolder = await context
                .ServiceProvider
                .GetService<ILockFacatory>()
                .TryCreateLockAsync(resource, TimeSpan.FromSeconds(5));

            if (lockHolder != null)
            {
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    await lockHolder.LockRelease();
                }
            }
             
            if (context.IsAsync())
            {
                context.ReturnValue = Task.CompletedTask;
            }
        }
    }
}
