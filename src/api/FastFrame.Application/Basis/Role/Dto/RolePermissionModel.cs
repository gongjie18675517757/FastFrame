using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class RolePermissionModel
    {
        /// <summary>
        /// 权限标记
        /// </summary> 
        public string PermissionKey { get; set; }

        /// <summary>
        /// 上级权限标记
        /// </summary> 
        public string SuperPermissionKey { get; set; }
    }
}
