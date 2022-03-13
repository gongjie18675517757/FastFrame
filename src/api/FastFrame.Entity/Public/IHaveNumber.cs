﻿namespace FastFrame.Entity
{
    /// <summary>
    /// 标识需要编号
    /// </summary>
    public interface IHaveNumber : IEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        string Number { set; get; }

        /// <summary>
        /// 表单名称
        /// </summary>
        string GetModuleName()
        {
            return this.GetType().Name;
        }
    }
}
