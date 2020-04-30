using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    public partial class RolePermissionModel : IDto
    {
        public string Id { get; set; }

        public string Super_Id { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool IsAuthorization { get; set; }

        /// <summary>
        /// 子级权限
        /// </summary>
        public IEnumerable<RolePermissionModel> Children { get; set; }
    
    }
}
