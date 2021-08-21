namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色成员
    /// </summary> 
    public class RoleMember : TableMap
    {
        /// <summary>
        /// 角色
        /// </summary> 
        public override string FKey_Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary> 
        public override string Value_Id { get; set; } 
    }
}
