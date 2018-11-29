using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 添加前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainAdding<T> : BaseEventData<T> where T : IDto
    {
        public DoMainAdding(T data, params object[] args) : base(data, args)
        {
        }
    }
}
