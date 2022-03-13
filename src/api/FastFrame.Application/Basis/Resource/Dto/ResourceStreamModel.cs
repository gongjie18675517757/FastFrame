using FastFrame.Infrastructure.Resource;
using System.IO;

namespace FastFrame.Application
{
    /// <summary>
    /// 文件流信息
    /// </summary>
    public partial class ResourceStreamModel : IResourceStreamInfo
    {
        public ResourceStreamModel(string name, string contentType, DateTime modifyTime, Stream resourceBlobStream)
        {
            Name = name;
            ModifyTime = modifyTime;
            ContentType = contentType;
            ResourceBlobStream = resourceBlobStream;
        }

        public string Name { get; }

        public Stream ResourceBlobStream { get; private set; }

        public DateTime ModifyTime { get; }

        public string ContentType { get; }

        public void ReplaceBlobStream(Stream input)
        {
            input.Position = 0;
            ResourceBlobStream = input;
        }
    }
}
