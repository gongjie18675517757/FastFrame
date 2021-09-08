using System;

namespace FastFrame.Infrastructure.Resource
{
    /// <summary>
    /// 资源信息
    /// </summary>
    public interface IResourceInfo
    {
        public string Id { get; set; }

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
}
