namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 审批时指定的下一步审核人
    /// </summary>
    public class FlowNextChecker : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowStep
        /// </summary>
        public string FlowStep_Id { get; set; }

        /// <summary>
        /// 关联:User
        /// </summary>
        public string Checker_Id { get; set; }
    }
}
