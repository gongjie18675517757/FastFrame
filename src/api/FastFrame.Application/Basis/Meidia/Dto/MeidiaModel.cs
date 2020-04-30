using System;

namespace FastFrame.Application.Basis
{
    public class MeidiaModel:IDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 上级
        /// </summary> 
        public string Super_Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 资源
        /// </summary> 
        public string Resource_Id { get; set; } 

        /// <summary>
        /// 是否文件夹
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        /// 资源大小
        /// </summary>
        public long? Size { get; set; }

        /// <summary>
        /// 资源标识
        /// </summary> 
        public string ContentType { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
