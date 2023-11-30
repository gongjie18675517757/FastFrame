namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 数据字典服务
    /// </summary>
    public interface IEnumItemProvider
    { 

        Task<IReadOnlyDictionary<int, string>> EnumValues(int enum_key);
    }
}