namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 角色成员
    /// </summary> 
    public class RoleMember : TableMap
    {
        /// <summary>
        /// 角色 <see cref="Role"/>
        /// </summary> 
        public override string FKey_Id { get => base.FKey_Id; set => base.FKey_Id = value; }

        /// <summary>
        /// 用户 <see cref="User"/>
        /// </summary> 
        public override string Value_Id { get => base.Value_Id; set => base.Value_Id = value; }
    }
}
