using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 标记为表
    /// </summary>
    public interface IEntity : IQuery
    {
        /// <summary>
        ///主键
        /// </summary>
        string Id { get; set; } 
    }



}
