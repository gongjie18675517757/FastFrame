using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 标记使用管理字段,记录创建人,创建时间,修改人,修改时间
    /// </summary>
    public interface IHasManage
    {
        /// <summary>
        /// 创建人
        /// </summary>        
        string Create_User_Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        string Modify_User_Id { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime ModifyTime { get; set; }
    }
}
