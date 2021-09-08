namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 节点审核角色
    /// </summary> 
    public class FlowNodeRole : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 关联:Role
        /// </summary>
        public string Role_Id { get; set; }
    }
}
