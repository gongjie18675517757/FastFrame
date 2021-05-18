﻿using FastFrame.Entity;
using FastFrame.Infrastructure.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Application
{
    /// <summary>
    /// 附件
    /// </summary>
    public partial class ResourceModel : IDto, IResourceInfo
    {
        public string Id { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        [Hide]
        public string Key { get; set; }

        public string FKey_Id { get; set; }

        /// <summary>
        /// 名称 
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 大小 
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 类型 
        /// </summary>
        [StringLength(150)]
        public string ContentType { get; set; }

        /// <summary>
        /// 上传人 
        /// </summary>
        public string UploaderName { get; set; }

        /// <summary>
        /// 上传时间 
        /// </summary>
        public DateTime UploadTime { get; set; }
    }
}
