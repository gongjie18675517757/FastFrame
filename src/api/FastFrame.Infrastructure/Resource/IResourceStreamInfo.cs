using System.IO;

namespace FastFrame.Infrastructure.Resource
{
    /// <summary>
    /// 资源流
    /// </summary>
    public interface IResourceStreamInfo
    {
        /// <summary>
        /// 名称 
        /// </summary> 
        string Name { get; }

        /// <summary>
        /// 资源流
        /// </summary>
        Stream ResourceBlobStream { get; }

        /// <summary>
        /// 替换资源
        /// </summary>
        /// <param name="input"></param>
        void ReplaceBlobStream(Stream input);
    }
}
