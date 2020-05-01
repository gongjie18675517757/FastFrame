namespace FastFrame.Entity
{
    /// <summary>
    /// 标记为部门单据
    /// </summary>
    public interface IHaveDept : IEntity
    {
        /// <summary>
        /// 获取归属科室
        /// </summary>
        string[] GetBeDeptIds();
    }
}
