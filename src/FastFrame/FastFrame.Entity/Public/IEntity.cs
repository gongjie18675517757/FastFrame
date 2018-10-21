using System;

namespace FastFrame.Entity
{
    public interface IEntity
    {
        /// <summary>
        ///主键
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        string OrganizeId { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
