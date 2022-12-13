using FastFrame.Entity.Enums;
using System.Threading.Tasks;

namespace FastFrame.Entity
{
    /// <summary>
    /// 标识需要审核
    /// </summary>
    public interface IHaveCheck : IEntity, IHaveNumber
    {
        /// <summary>
        /// 创建人
        /// </summary>        
        string Create_User_Id { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        [EnumItem(EnumName.FlowStatusEnum)]
        int FlowStatus { get; set; }

        /// <summary>
        /// 获取摘要
        /// </summary>
        /// <returns></returns>
        string GetDescription();
    }
}
