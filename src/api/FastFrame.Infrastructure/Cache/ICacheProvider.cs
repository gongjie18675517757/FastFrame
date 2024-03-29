﻿using System;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Cache
{
    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICacheProvider
    {
        Task SetAsync<T>(string key, T val, TimeSpan? expire);


        Task<T> GetAsync<T>(string key);


        Task<T> HGetAsync<T>(string key, string field);


        Task HSetAsync<T>(string key, string field, T val, TimeSpan? expire); 


        Task DelAsync(string key);


        Task ListPushAsync<T>(string key, T val);


        Task<long> ListLengthAsync(string key);


        Task<T> ListPopAsync<T>(string key);
    }
}
