using FastFrame.Entity.Basis;
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

    public partial class MeidiaOutput
    {
        public Meidia Curr { get; set; }

        public IEnumerable<MeidiaDto> Children { get; set; }
    }
}
