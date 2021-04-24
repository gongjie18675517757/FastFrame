using System;
using System.IO;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 附件资源操作提供者
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// 保存
        /// </summary> 
        Task<string> WriteAsync(Stream stream);

        /// <summary>
        /// 读取
        /// </summary> 
        Task<Stream> ReadAsync(string relativelyPath);

        /// <summary>
        /// 检查是否存在
        /// </summary> 
        Task<bool> ExistsAsync(string relativelyPath);

        /// <summary>
        /// 获取实际文件路径
        /// </summary>
        /// <param name="relativelyPath"></param>
        /// <returns></returns>
        string GetFilePath(string relativelyPath);
    }

    /// <summary>
    /// 资源信息
    /// </summary>
    public interface IResourceInfo
    {
        /// <summary>
		/// 名称 
		/// </summary> 
		public string Name { get; }

        /// <summary>
        /// 大小 
        /// </summary>
        public long Size { get; }

        /// <summary>
        /// 类型 
        /// </summary> 
        public string ContentType { get; }

        /// <summary>
        /// 上传人 
        /// </summary>
        public string UploaderName { get; }

        /// <summary>
        /// 上传时间 
        /// </summary>
        public DateTime UploadTime { get; }
    }

    /// <summary>
    /// 资源流
    /// </summary>
    public interface IResourceStreamInfo
    {
        /// <summary>
        /// 名称 
        /// </summary> 
        public string Name { get; } 

        /// <summary>
        /// 资源流
        /// </summary>
        public Stream ResourceBlobStream { get; }

        ///// <summary>
        ///// 内容类型
        ///// </summary>
        //public string ContentType { get; set; }
    }

    /// <summary>
    /// 资源存储
    /// </summary>
    public interface IResourceStoreProvider
    {
        /// <summary>
        /// 尝试保存资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contentType"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<IResourceInfo> TrySaveResource(string name, string contentType, Stream stream);

        /// <summary>
        /// 尝试获取资源
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        Task<IResourceStreamInfo> TryGetResource(string resourceId);
    }
}
