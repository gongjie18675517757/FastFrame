using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 编号记录
    /// </summary> 
    public class NumberRecord : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [StringLength(50)]
        public string BeModule { get; set; }

        /// <summary>
        /// 年
        /// </summary> 
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 流水
        /// </summary>
        public int Serial { get; set; }

        /// <summary>
        /// 上一期流水
        /// </summary>
        public int PrevSerial { get; set; }


        public string Tenant_Id { get; set; }
    }
}
