namespace FastFrame.Infrastructure.Resource
{
    /// <summary>
    /// 文件读取器
    /// </summary>
    public interface IResourceReader
    {
        /// <summary>
        /// 验证资源是否还存在
        /// </summary>
        /// <returns></returns>
        bool Exists();

        /// <summary>
        /// 获取流
        /// </summary>
        /// <returns></returns>
        bool TryGetStream(out Stream stream);

        /// <summary>
        /// 尝试获取本地文件名(绝对路径)
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns></returns>
        bool TryGetLocalFileFullName(out string file_name);

        /// <summary>
        /// 尝试获取相对路径
        /// </summary>
        /// <param name="relativelyPath"></param>
        /// <returns></returns>
        bool TryGetRelativelyPath(out string relativelyPath);
    }
}
