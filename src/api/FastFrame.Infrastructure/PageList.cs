using System.Collections.Generic;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 分页返回内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPageList<T>
    {
        /// <summary>
        /// 返回查询到的总记录数量
        /// </summary> 
        int Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        IReadOnlyList<T> Data { get; set; }  
    }
}
