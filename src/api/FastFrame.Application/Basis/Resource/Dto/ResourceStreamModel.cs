using FastFrame.Infrastructure.Interface;
using System.IO;

namespace FastFrame.Application
{
    /// <summary>
    /// 文件流信息
    /// </summary>
    public partial class ResourceStreamModel : IResourceStreamInfo
    {
        public ResourceStreamModel(string name,   Stream resourceBlobStream)
        {
            Name = name; 
            ResourceBlobStream = resourceBlobStream;
        }

        public string Name { get; } 

        public Stream ResourceBlobStream { get; }
    }
}
