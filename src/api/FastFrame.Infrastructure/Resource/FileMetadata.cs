namespace FastFrame.Infrastructure.Resource
{
    /// <summary>
    /// 文件元数据
    /// </summary>
    public class FileMetadata
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 分片文件大小
        /// </summary>
        public int TotalChunkFiles { get; set; }

        /// <summary>
        /// 文件的md5值
        /// </summary>
        public string FileMD5 { get; set; }

        /// <summary>
        /// 文件分片索引的完成状态
        /// </summary>
        public bool[] ComplateIndexs { get; set; }
    }
}
