namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程步骤审核人
    /// </summary> 
    public class FlowStepChecker : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：FlowStep
        /// </summary>
        public string FlowStep_Id { get; set; }

        /// <summary>
        /// 关联：User  
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 单据ID
        /// </summary> 
        public string Bill_Id { get; set; }

        /// <summary>
        /// 关联：FlowInstance
        /// </summary>
        public string FlowInstance_Id { get; set; }
    }
}
