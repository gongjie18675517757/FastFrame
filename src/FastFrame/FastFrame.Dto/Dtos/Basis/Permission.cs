using System.Collections.Generic;

namespace FastFrame.Dto.Basis
{
    public partial class PermissionDto
    {
        /// <summary>
        /// 下级权限
        /// </summary>
        public IEnumerable<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
    
}
