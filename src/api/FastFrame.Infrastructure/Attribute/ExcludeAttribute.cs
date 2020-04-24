﻿using System;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 排除标识[不生成DTO]
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ExcludeAttribute : Attribute
    {
    }
}
