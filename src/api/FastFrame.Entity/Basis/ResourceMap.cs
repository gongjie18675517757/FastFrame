using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 资源映射
    /// </summary>
    public class ResourceMap : TableMap
    {
        /// <summary>
        /// 外键ID
        /// </summary>
        public override string FKey_Id { get => base.FKey_Id; set => base.FKey_Id = value; }


        /// <summary>
        /// 关联到<see cref="Resource"/>
        /// </summary>
        public override string Value_Id { get => base.Value_Id; set => base.Value_Id = value; }
    }
}
