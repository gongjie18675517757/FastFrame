using FastFrame.Application.Basis;
using System.Collections.Generic;

namespace FastFrame.Application
{
    /// <summary>
    /// 标记有附件
    /// </summary>
    public interface IHaveMultiFileDto:IDto
    {
        /// <summary>
        /// 附件
        /// </summary>
        public IEnumerable<ResourceModel> Files { get; set; }
    }
}
