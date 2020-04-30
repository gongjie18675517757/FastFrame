using FastFrame.Entity;
using System;

namespace FastFrame.Application
{
    /// <summary>
    /// 基类DTO
    /// </summary>
    public abstract class BaseDto : IDto
    { 
        public string Id { get; set; }
    }

    /// <summary>
    /// 基类DTO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDto<T> : BaseDto, IDto<T> where T : class, IEntity
    {

    }
}
