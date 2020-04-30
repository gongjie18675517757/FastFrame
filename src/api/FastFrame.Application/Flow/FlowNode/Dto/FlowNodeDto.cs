using FastFrame.Application.Basis;
using System.Collections.Generic;

namespace FastFrame.Application.Flow
{
    public partial class FlowNodeDto
    {
        /// <summary>
        /// 节点审核角色
        /// </summary>
        public IEnumerable<RoleViewModel> Roles { get; set; }

        /// <summary>
        /// 节点审核人
        /// </summary>
        public IEnumerable<UserViewModel> Users { get; set; }

        /// <summary>
        /// 节点动态审核人
        /// </summary>
        public IEnumerable<string> Fields { get; set; }
    }
}
