namespace FastFrame.Entity
{
    /// <summary>
    /// 标识需要编号
    /// </summary>
    public interface IHaveNumber : IEntity
    { 
        /// <summary>
        /// 设置编号
        /// </summary>
        void SetNumber(string val);

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <returns></returns>
        string GetNumber();

        /// <summary>
        /// 表单名称
        /// </summary>
        string GetModuleName()
        {
            return this.GetType().Name;
        }
    }
}
