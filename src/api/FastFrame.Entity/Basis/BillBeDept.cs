namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 单据归属部门
    /// </summary>
    public class BillBeDept : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联：Bill
        /// </summary>
        public string Bill_Id { get; set; }

        /// <summary>
        /// 归属部门
        /// </summary> 
        public string BeDept_Id { get; set; }
    }
}
