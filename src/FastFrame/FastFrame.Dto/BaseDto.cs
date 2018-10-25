using FastFrame.Entity;
using System;

namespace FastFrame.Dto
{
    /// <summary>
    /// 基类DTO
    /// </summary>
    public abstract class BaseDto : IDto
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateAccount { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary> 
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyAccount { get; set; }

        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string ModifyName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

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
