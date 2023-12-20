namespace FastFrame.Entity
{
    /// <summary>
    /// 标记与下一个属性组成范围值,生成DTO时会组合在一起
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RangeValueBeginAttribute : Attribute
    {
        
    }
}
