using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Attrs;
using System;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity
{
    public abstract class BaseEntity : IEntity, IHasTenant, IHasSoftDelete,IHasManage, IHaveConcurrencyCheck
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; } 

        /// <summary>
        /// 创建人
        /// </summary>        
        [RelatedTo(typeof(User))]
        [ReadOnly]
        [Hide(HideMark.Form)]
        public string Create_User_Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [ReadOnly]
        [Hide(HideMark.Form)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [RelatedTo(typeof(User))]
        [ReadOnly]
        [Hide(HideMark.Form)]
        public string Modify_User_Id { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [ReadOnly]
        [Hide(HideMark.Form)]
        public DateTime ModifyTime { get; set; }
    }
}
