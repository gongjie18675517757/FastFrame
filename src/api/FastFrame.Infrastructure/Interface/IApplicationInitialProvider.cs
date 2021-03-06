﻿using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 应用程序初始化时
    /// </summary>
    public interface IApplicationInitialProvider
    {
        /// <summary>
        /// 执行初始化
        /// 注意：要自己处理异常
        /// </summary>
        /// <returns></returns>
        Task InitialAsync();
    }
}
