using FastFrame.Infrastructure.Resource;
using System.IO;

namespace FastFrame.Application
{
    /// <summary>
    /// 本地文件流信息
    /// </summary>
    public partial class LocalFileResourceStreamModel : IResourceRedearInfo
    {
        private readonly IResourceReader fileReader;

        public LocalFileResourceStreamModel(string name, string contentType, DateTime modifyTime, IResourceReader fileReader)
        {
            Name = name;
            ModifyTime = modifyTime;
            this.fileReader = fileReader;
            ContentType = contentType;
        }

        public string Name { get; }

        public DateTime ModifyTime { get; }

        public string ContentType { get; }

        public IResourceReader GetResourceReader()
        {
            return fileReader;
        }
    }
}
