using System.IO;

namespace FastFrame.Infrastructure.Resource
{
    /// <summary>
    /// 资源信息
    /// </summary>
    public interface IResourceRedearInfo
    {
        /// <summary>
        /// 名称 
        /// </summary> 
        string Name { get; }

        /// <summary>
        /// 资源类型
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime ModifyTime { get; }

        /// <summary>
        /// 获取资源读取器
        /// </summary>
        IResourceReader GetResourceReader();
    }
}
