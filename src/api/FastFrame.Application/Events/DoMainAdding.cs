using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 添加前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainAdding<T> : BaseEventData<T>  
    {
        public DoMainAdding(T data, params object[] args)
        {
            Data = data;
            Args = args;
        }

        public T Data { get; }
        public object[] Args { get; }
    }
}
