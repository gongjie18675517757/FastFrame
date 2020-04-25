namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 节点审核人
    /// </summary> 
    public class FlowNodeUser : IEntity, IHasSoftDelete, IHasTenant
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 关联:User
        /// </summary>
        public string User_Id { get; set; }
    }
}
