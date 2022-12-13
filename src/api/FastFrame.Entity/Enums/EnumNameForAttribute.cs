namespace FastFrame.Entity.Enums
{
    /// <summary>
    /// 枚举映射
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    [System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumNameForAttribute<TEnum> : Attribute
        where TEnum : struct,Enum
    {
    }
}
